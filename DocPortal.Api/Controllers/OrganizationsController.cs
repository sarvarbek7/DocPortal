using DocPortal.Api.QueryServices;
using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Application.Services.Processing;
using DocPortal.Contracts.Dtos;
using DocPortal.Contracts.Endpoints.Organizations;
using DocPortal.Contracts.Endpoints.Organizations.Options;
using DocPortal.Domain.Common;
using DocPortal.Domain.Entities;

using ErrorOr;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Api.Controllers;

[Authorize(Roles = Role.SuperAdmin)]
[Route("api/[controller]")]
public class OrganizationsController(IOrganizationService organizationService,
                                     IStatisticsService statisticsService,
                                     IUserOrganizationService userOrganizationService,
                                     IQueryService<Organization> queryService,
                                     IMapper mapper) : _ApiController
{
  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetAllOrganizations([FromQuery] int? limit,
                                           [FromQuery] int? page,
                                           [FromQuery] string? title,
                                           [FromQuery] int? parentId)
  {
    try
    {
      var pageOptions = new PageOptions(limit, page);

      var filterOptions = new OrganizationFilterOptions(title, parentId);
      var includeQueryOptions = new OrganizationIncludeQueryOptions();

      var predicate =
        queryService.ApplyFilterOptions(filterOptions);

      ICollection<string>? includedNavigationalProperties =
        queryService.ApplyIncludeQueries(includeQueryOptions);

      var storedOrganizations = organizationService.RetrieveAll(pageOptions,
                                      predicate,
                                      asNoTracking: false,
                                      includedNavigationalProperties);

      var organizationDtos =
        mapper.Map<IEnumerable<OrganizationDto>>(storedOrganizations);

      var response = new GetAllOrganizationsResponse(organizationDtos,
                                                  statisticsService.GetOrganizationsCount(predicate),
                                                  pageOptions.PageSize);

      return Ok(response);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [AllowAnonymous]
  [HttpGet("{id:int:required}")]
  public async ValueTask<IActionResult> GetOrganizationByIdAsync(int id,
                                                                 [FromQuery] bool includeAdmins = false,
                                                                 [FromQuery] bool includeDocuments = false,
                                                                 [FromQuery] bool includeSubordinates = false)
  {
    try
    {
      OrganizationIncludeQueryOptions queryOptions = new OrganizationIncludeQueryOptions(includeAdmins, includeDocuments, includeSubordinates);

      ICollection<string>? includedNavigationalProperties =
        queryService.ApplyIncludeQueries(queryOptions);


      var storedOrganizationOrError =
        await organizationService.RetrieveOrganizationByIdWithDetails(id,
                                                          false,
                                                          includedNavigationalProperties);

      return storedOrganizationOrError.Match(
        onValue: value =>
          Ok(new GetOrganizationByIdResponse(
            Organization: mapper.Map<OrganizationDto>(value),
            Parent: mapper.Map<OrganizationDto>(value.PrimaryOrganization),
            Subordinates: mapper.Map<IEnumerable<OrganizationDto>>(value.Subordinates),
            Admins: mapper.Map<IEnumerable<UserOrganizationDto>>(value.Admins),
            Documents: mapper.Map<IEnumerable<DocumentDto>>(value.Documents))),
        onError: Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [HttpPost]
  public async ValueTask<IActionResult> PostOrganization(OrganizationDto request)
  {
    try
    {
      var organization = mapper.Map<Organization>(request);

      var createdOrganizationOrError =
        await organizationService.AddEntityAsync(organization);

      return createdOrganizationOrError.Match(
        value => Ok(mapper.Map<OrganizationDto>(value)),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [HttpPut]
  public async ValueTask<IActionResult> PutOrganization(OrganizationDto request)
  {
    try
    {
      var organization = mapper.Map<Organization>(request);

      var modifiedOrganizationOrError =
        await organizationService.ModifyAsync(organization);

      return modifiedOrganizationOrError.Match(
        value => Ok(mapper.Map<OrganizationDto>(value)),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected(description: ex.Message)]);
    }
  }

  [HttpDelete("{id:int:required}")]
  public async ValueTask<IActionResult> DeleteOrganization(int id)
  {
    try
    {
      var deletedOrganizationOrError =
        await organizationService.RemoveByIdAsync(id);

      return deletedOrganizationOrError.Match(
        value => Ok(mapper.Map<OrganizationDto>(value)),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [HttpPost("{organizationId:int:required}/assign-users")]
  public async ValueTask<IActionResult> AssignOrganizations(int organizationId, IEnumerable<int> userIds)
  {
    try
    {
      var errorOrOrganization =
        await organizationService.RetrieveOrganizationByIdWithDetails(organizationId, false, [nameof(Domain.Entities.Organization.Admins)]);

      if (errorOrOrganization.IsError)
      {
        return Problem(errorOrOrganization.Errors);
      }

      Organization organization = errorOrOrganization.Value;

      if (organization.Admins is { Count: > 0 } admins)
      {
        userIds =
          userIds.Where(id => !admins.Select(admin => admin.Id).Contains(id));
      }

      if (userIds.Any())
      {
        return Problem([OrganizationError.AlreadyAssignedToUsers]);
      }

      var userOrganizations = (await userOrganizationService.AddMultipleUserOrganizationsAsync(
        userIds.Select(id => new UserOrganization()
        {
          OrganizationId = organizationId,
          UserId = id
        }))).ToList();

      return Ok(mapper.Map<List<OrganizationDto>>(userOrganizations));
    }
    catch (DbUpdateException ex)
      when (ex.InnerException is Npgsql.PostgresException inner && inner.SqlState == "23503")
    {
      return Problem([Error.NotFound("AssignedOrganizations.NotFound", description: "Some of assigned users not found in database")]);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }
}
