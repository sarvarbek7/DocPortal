using System.Linq.Expressions;

using DocPortal.Api.Filters;
using DocPortal.Api.Http;
using DocPortal.Api.QueryServices;
using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Application.Services.Processing;
using DocPortal.Contracts.Dtos;
using DocPortal.Contracts.Endpoints.Documents;
using DocPortal.Contracts.Endpoints.Documents.Options;
using DocPortal.Domain.Common;
using DocPortal.Domain.Entities;

using ErrorOr;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Api.Controllers;

[Authorize(Roles = $"{Role.SuperAdmin}, {Role.Admin}")]
[Route("api/[controller]")]
public class DocumentsController(IDocumentService documentService,
                                 IStatisticsService statisticsService,
                                 IDocumentTypeService documentTypeService,
                                 IDeletedEntitesService deletedEntitesService,
                                 [FromKeyedServices("documentAudit")] IAuditService<Guid> auditService,
                                 IQueryService<Document> queryService,
                                 IMapper mapper,
                                 IWebHostEnvironment environment) : _ApiController
{
  private readonly string[] _availableDocumentTypes =
  [
    ".doc",
    ".docx",
    ".xlsx",
    ".xls",
    ".pdf",
    ".txt",
    ".epub",
    ".ppt"
  ];

  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetAllDocuments([FromQuery] int? limit,
                                       [FromQuery] int? page,
                                       [FromQuery] int? organizationId,
                                       [FromQuery] int? documentTypeId,
                                       [FromQuery] string? title,
                                       [FromQuery] string? registerNumber,
                                       [FromQuery] DateOnly? startDate,
                                       [FromQuery] DateOnly? endDate,
                                       [FromQuery] string? orderby,
                                       [FromQuery] bool isDescending = false,
                                       [FromQuery] bool includeDocumentType = false,
                                       [FromQuery] bool includeOrganization = false)
  {
    try
    {
      var pageOptions = new PageOptions(
        limit,
        page);

      var documentFilterOptions = new DocumentFilterOptions(title,
                                                            registerNumber,
                                                            organizationId,
                                                            documentTypeId,
                                                            startDate,
                                                            endDate);

      Expression<Func<Document, bool>>? predicate =
        queryService.ApplyFilterOptions(documentFilterOptions);

      int total = statisticsService.GetDocumentsCount(predicate);

      var documentIncludeQueryOptions = new DocumentIncludeQueryOptions(includeDocumentType,
                                                                        includeOrganization);

      ICollection<string> includedNavigationalProperties =
        queryService.ApplyIncludeQueries(documentIncludeQueryOptions);

      var orderFunc = queryService.ApplyOrderbyQuery(orderby, isDescending);

      var documents =
        documentService.RetrieveAll(pageOptions, predicate, asNoTracking: false, includedNavigationalProperties, orderFunc);

      return Ok(new GetAllDocumentsResponse(
        mapper.Map<IEnumerable<DocumentDto>>(documents),
        total,
        pageOptions.PageSize));
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AllowAnonymous]
  [HttpGet("{id:guid:required}")]
  public async ValueTask<IActionResult> GetDocumentByIdAsync(Guid id)
  {
    try
    {
      var queryOptions =
      new DocumentIncludeQueryOptions(true, false);

      ICollection<string>? includedNavigationalProperties =
        queryService.ApplyIncludeQueries(queryOptions);

      var errorOrDocument =
        await documentService.RetrieveDocumentByIdWithDetailsAsync(id, false, default, includedNavigationalProperties);

      return errorOrDocument.Match(
        value => Ok(mapper.Map<DocumentDto>(value)),
        Problem);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AllowAnonymous]
  [HttpGet("types")]
  public IActionResult GetDocumentTypes()
  {
    try
    {
      PageOptions pageOptions = null;

      var documentTypes =
        documentTypeService.RetrieveAll(pageOptions, ignorePagination: true);

      return Ok(new
      {
        DocumentTypes = mapper.Map<List<DocumentTypeDto>>(documentTypes),
        Total = statisticsService.GetDocumentTypesCount()
      });
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AdminOrganizationAuthorize]
  [HttpPost]
  public async ValueTask<IActionResult> UploadDocuments([FromForm] UploadDocumentsRequest request)
  {
    try
    {
      List<Document> documents = [];

      string relativePath = Path.Combine("documents",
        request.OrganizationId.ToString().PadLeft(4, '0'));

      Directory.CreateDirectory(Path.Combine(environment.ContentRootPath, relativePath));

      var files = HttpContext.Request.Form.Files;
      var documentDtos = request.Documents;

      int index = 0;


      foreach (var file in files)
      {
        var documentDto = documentDtos[index++];

        var extension = Path.GetExtension(file.FileName);

        if (!_availableDocumentTypes.Contains(extension))
        {
          return Problem([Error.Validation("Document.WrongType", extension)]);
        }

        var fileName = $"{Guid.NewGuid()}{extension}";

        Document document = new()
        {
          OrganizationId = request.OrganizationId,
          DocumentTypeId = documentDto.DocumentTypeId,
          StoragePath = Path.Combine(relativePath, fileName),
          Title = documentDto.Title,
          RegisteredDate = documentDto.RegisteredDate,
          RegisteredNumber = documentDto.RegisteredNumber,
          CreatedBy = request.UserId,
          UpdatedBy = request.UserId
        };

        documents.Add(document);
      }

      await documentService.AddMultipleDocuments(documents);

      index = 0;
      foreach (var file in files)
      {
        using (var stream = new FileStream(
          Path.Combine(environment.ContentRootPath, documents[index++].StoragePath), FileMode.Create))
        {
          file.CopyTo(stream);
        }
      }

      return Ok(mapper.Map<List<DocumentDto>>(documents));
    }
    catch (Npgsql.PostgresException ex)
      when (ex.SqlState == "23503")
    {
      return Problem([OrganizationError.NotFound]);
    }
    catch
    {
      return Problem([Error.Unexpected()]);

    }
  }

  [AdminOrganizationAuthorize]
  [HttpDelete("{id:guid:required}")]
  public async ValueTask<IActionResult> DeleteDocumentById(Guid id)
  {
    try
    {
      int? deletedBy = null;

      var httpUserId =
              HttpContextService.GetUserId(HttpContext);

      if (int.TryParse(httpUserId, out int adminId))
      {
        deletedBy = adminId;
      }

      var errorOrDeletedDocument =
        await documentService.RemoveByIdAsync(id, deletedBy: deletedBy);

      return errorOrDeletedDocument.Match(
        value => Ok(mapper.Map<DocumentDto>(value)),
        Problem);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AdminOrganizationAuthorize]
  [HttpPut]
  public async ValueTask<IActionResult> UpdateDocument(DocumentDto document)
  {
    try
    {
      var documentFromDto = mapper.Map<Document>(document);

      var httpUserId =
              HttpContextService.GetUserId(HttpContext);

      if (int.TryParse(httpUserId, out int adminId))
      {
        documentFromDto.UpdatedBy = adminId;
      }

      var errorOrUpdatedDocument =
        await documentService.ModifyAsync(documentFromDto);

      return errorOrUpdatedDocument.Match(
        value => Ok(mapper.Map<DocumentDto>(value)),
        Problem);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AllowAnonymous]
  [HttpGet("download")]
  public async ValueTask<IActionResult> DownloadDocument([FromQuery] Guid id)
  {
    //FileStream stream = null;

    try
    {
      ErrorOr<Document?> errorOrStoredDocument =
        await documentService.RetrieveByIdAsync(id);

      if (errorOrStoredDocument.IsError)
      {
        return Problem(errorOrStoredDocument.Errors);
      }

      Document? storedDocument = errorOrStoredDocument.Value;

      if (storedDocument is null || storedDocument.IsDeleted)
      {
        return NotFound();
      }

      if (storedDocument.IsPrivate)
      {
        return BadRequest("Maxfiy hujjat.");
      }

      var contentPath = environment.ContentRootPath;

      var documentPath =
        Path.Combine(contentPath, storedDocument.StoragePath);

      if (!System.IO.File.Exists(documentPath))
      {
        return NotFound();
      }

      string extension = Path.GetExtension(storedDocument.StoragePath);

      var stream = new FileStream(documentPath,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.Read);

      return File(stream, "application/octet-stream", fileDownloadName: $"{storedDocument.Title}{extension}");
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpGet("{id:guid:required}/audit")]
  public async ValueTask<IActionResult> GetAuditDetails(Guid id)
  {
    try
    {
      var auditInfo = await auditService.BasicAuditInfo(id);

      return Ok(new
      {
        CreatedBy = auditInfo.Item1.CreatedBy,
        CreatedByFullName = auditInfo.Item1.CreatedByFullName,
        CreatedAt = auditInfo.Item1.CreateAt,
        UpdatedBy = auditInfo.Item2.UpdatedBy,
        UpdatedByFullName = auditInfo.Item2.UpdatedByFullName,
        UpdatedAt = auditInfo.Item2.UpdatedAt
      });
    }
    catch
    {
      return Problem("Something went wrong to load audit details");
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpGet("{id:guid:required}/deleted-audit")]
  public async ValueTask<IActionResult> GetAuditDeleted(Guid id)
  {
    try
    {
      var auditInfo = await auditService.DeletedAuditInfo<Document>(id);

      return Ok(auditInfo);
    }
    catch
    {
      return Problem("Something went wrong to load audit details");
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpGet("deleted")]
  public IActionResult GetAllDeletedDocuments([FromQuery] int? limit,
                                       [FromQuery] int? page,
                                       [FromQuery] int? organizationId,
                                       [FromQuery] int? documentTypeId,
                                       [FromQuery] string? title,
                                       [FromQuery] string? registerNumber,
                                       [FromQuery] DateOnly? startDate,
                                       [FromQuery] DateOnly? endDate,
                                       [FromQuery] string? orderby,
                                       [FromQuery] bool isDescending = false)
  {
    try
    {
      var pageOptions = new PageOptions(
        limit,
        page);

      var documentFilterOptions = new DocumentFilterOptions(title,
                                                            registerNumber,
                                                            organizationId,
                                                            documentTypeId,
                                                            startDate,
                                                            endDate,
                                                            true);

      Expression<Func<Document, bool>>? predicate =
        queryService.ApplyFilterOptions(documentFilterOptions);

      int total = statisticsService.GetDocumentsCount(predicate);

      var documentIncludeQueryOptions = new DocumentIncludeQueryOptions(true, false);

      ICollection<string> includedNavigationalProperties =
        queryService.ApplyIncludeQueries(documentIncludeQueryOptions);

      var orderFunc = queryService.ApplyOrderbyQuery(orderby, isDescending);

      var documents =
        deletedEntitesService.RetrieveDeletedEntities(pageOptions,
                                                      predicate,
                                                      asNoTracking: false,
                                                      orderFunc,
                                                      ignorePagination: false);

      return Ok(new GetAllDocumentsResponse(
        mapper.Map<IEnumerable<DocumentDto>>(documents),
        total,
        pageOptions.PageSize));
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpGet("restore/{id:guid:required}")]
  public async ValueTask<IActionResult> RestoreDocument(Guid id)
  {
    try
    {
      int? updatedBy = null;

      var httpUserId =
              HttpContextService.GetUserId(HttpContext);

      if (int.TryParse(httpUserId, out int adminId))
      {
        updatedBy = adminId;
      }

      await deletedEntitesService.RestoreEntity<Document, int>(new Document { Id = id, UpdatedBy = updatedBy });

      return NoContent();
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }
}
