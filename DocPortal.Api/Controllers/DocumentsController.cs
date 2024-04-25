using System.Linq.Expressions;

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
                                 IQueryService<Document> queryService,
                                 IMapper mapper,
                                 IWebHostEnvironment environment) : _ApiController
{
  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetAllDocuments([FromQuery] GetAllDocumentsRequest request)
  {
    try
    {
      var pageOptions = new PageOptions(
        request.PaginationOptions?.Limit,
        request.PaginationOptions?.Page);

      Expression<Func<Document, bool>>? predicate =
        queryService.ApplyFilterOptions(request.DocumentFilterOptions);

      int total = statisticsService.GetDocumentsCount(predicate);

      ICollection<string> includedNavigationalProperties =
        queryService.ApplyIncludeQueries(request.DocumentIncludeQueryOptions);

      var documents =
        documentService.RetrieveAll(pageOptions, predicate, asNoTracking: false, includedNavigationalProperties);

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
      new DocumentIncludeQueryOptions(true, true);

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
  public async ValueTask<IActionResult> GetDocumentTypes()
  {
    try
    {
      PageOptions pageOptions = null;

      var documentTypes =
        documentTypeService.RetrieveAll(pageOptions);

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

  [HttpPost]
  public async ValueTask<IActionResult> UploadDocuments([FromForm] UploadDocumentsRequest request)
  {
    try
    {
      List<Document> documents = [];

      string relativePath = Path.Combine("documents", request.OrganizationId.ToString().PadLeft(4, '0'));

      Directory.CreateDirectory(Path.Combine(environment.ContentRootPath, relativePath));

      var files = HttpContext.Request.Form.Files;
      var documentDtos = request.Documents;

      int index = 0;


      foreach (var file in files)
      {
        var documentDto = documentDtos[index++];

        var extension = Path.GetExtension(file.FileName);

        var fileName = $"{Guid.NewGuid()}.{extension}";

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


}
