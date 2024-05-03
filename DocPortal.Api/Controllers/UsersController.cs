using System.Linq.Expressions;

using DocPortal.Api.Http;
using DocPortal.Api.QueryServices;
using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Application.Services.Processing;
using DocPortal.Contracts.Dtos;
using DocPortal.Contracts.Endpoints.Users;
using DocPortal.Contracts.Endpoints.Users.Options;
using DocPortal.Domain.Common;
using DocPortal.Domain.Entities;

using ErrorOr;

using MapsterMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Api.Controllers;

[Route("api/[controller]")]
public class UsersController(IUserService userService,
                             IUserOrganizationService userOrganizationService,
                             IStatisticsService statisticsService,
                             IQueryService<User> queryService,
                             IMapper mapper) : _ApiController
{
  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetAllUsers([FromQuery] int? limit,
                                   [FromQuery] int? page,
                                   [FromQuery] string? keyword,
                                   [FromQuery] string? orderby,
                                   [FromQuery] bool isDescending = false)
  {
    try
    {
      var pageOptions = new PageOptions(limit, page);

      var filterOptions = new UserFilterOptions(keyword);

      var predicate =
        queryService.ApplyFilterOptions(filterOptions);

      var orderFunc =
        queryService.ApplyOrderbyQuery(orderby, isDescending);

      var storedUsers = userService.RetrieveAll(pageOptions,
                                      predicate,
                                      orderFunc: orderFunc);

      var userDtos =
        mapper.Map<IEnumerable<UserDto>>(storedUsers);

      var response = new GetAllUsersResponse(
        Users: userDtos,
        Total: statisticsService.GetUsersCount(predicate),
        PageSize: pageOptions.PageSize);

      return Ok(response);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected(description: ex.Message)]);
    }
  }

  [Authorize(Roles = $"{Role.Admin},{Role.SuperAdmin}")]
  [HttpGet("{id:int:required}")]
  public async ValueTask<IActionResult> GetUserByIdAsync(int id)
  {
    try
    {
      var queryOptions = new UserIncludeQueryOptions(true, true);
      ICollection<string> includedProperties =
      queryService.ApplyIncludeQueries(queryOptions);

      ErrorOr<User> storedUserOrError =
        await userService.RetrieveUserByIdWithDetailsAsync(id, false, includedProperties);

      return storedUserOrError.Match(
        onValue: value => Ok(new GetUserByIdResponse(
          User: mapper.Map<UserDto>(value),
          Login: value.UserCredential?.Login,
          Organizations: mapper.Map<IEnumerable<UserOrganizationDto>>(value.UserOrganizations))),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = $"{Role.Admin},{Role.SuperAdmin}")]
  [HttpGet("{id:int:required}/organizations")]
  public async ValueTask<IActionResult> GetUserOrganizations(int id,
                                                             [FromQuery] int? limit,
                                                             [FromQuery] int? page)
  {
    try
    {
      var pageOptions = new PageOptions(limit, page);

      ICollection<string> includedProperties = [nameof(UserOrganization.AssignedOrganization)];

      List<UserOrganization> userOrgs =
        userOrganizationService.RetrieveAll(pageOptions,
                                            predicate: userOrg => userOrg.UserId == id,
                                            includedNavigationalProperties: includedProperties).ToList();

      List<Organization> organizations =
        userOrgs.Select(userOrg => userOrg.AssignedOrganization).ToList();

      Expression<Func<Document, bool>> predicate =
        (Document doc) => organizations.Select(org => org.Id).Contains(doc.OrganizationId);

      var counts =
        statisticsService.GetDocumentsCountGroupByOrganization(predicate);

      return Ok(new GetUserOrganizationsResponse(
        Organizations: organizations.Select(organization =>
        {
          var subordinates = statisticsService.GetSubordinates(organization.Id);

          return new GetUserOrganizationsResponse.Organization(
          Id: organization.Id,
          Title: organization.Title,
          PhysicalIdentity: organization.PhysicalIdentity,
          PrimaryOrganizationId: organization.PrimaryOrganizationId,
          PrimaryOrganizationTitle: organization.PrimaryOrganization?.Title,
          Details: organization.Details,
          SubordinatesCount: subordinates.Count,
          DocumentsCount: counts.Find(docC => docC.OrganizationId == organization.Id)?.Count ?? 0,
          DocumentsCountIncludeSubordinates: counts.Where(count =>
            subordinates.Contains(count.OrganizationId) || count.OrganizationId == organization.Id).Sum(docCount => docCount.Count)
          );

        }).ToList()
    ));
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpPost]
  public async ValueTask<IActionResult> PostUser(UserDto request)
  {
    try
    {
      var user = mapper.Map<User>(request);

      var httpUserId =
              HttpContextService.GetUserId(HttpContext);

      if (int.TryParse(httpUserId, out int adminId))
      {
        user.CreatedBy = adminId;
        user.UpdatedBy = adminId;
      }

      var createdUserOrError =
        await userService.AddEntityAsync(user);

      return createdUserOrError.Match(
        value => Ok(mapper.Map<UserDto>(value)),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpPut]
  public async ValueTask<IActionResult> PutUser(UserDto request)
  {
    try
    {
      var user = mapper.Map<User>(request);

      var modifiedOrganizationOrError =
        await userService.ModifyAsync(user);

      return modifiedOrganizationOrError.Match(
        value => Ok(mapper.Map<UserDto>(value)),
        Problem);
    }
    catch (Exception ex)
    {
      return Problem([Error.Unexpected(description: ex.Message)]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpDelete("{id:int:required}")]
  public async ValueTask<IActionResult> DeleteUser(int id)
  {
    try
    {
      ErrorOr<User> deletedUserOrError =
        await userService.RemoveByIdAsync(id);

      return deletedUserOrError.Match(
        onValue: value => Ok(mapper.Map<UserDto>(value)),
        Problem);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpPost("{id:int:required}/assign-organizations")]
  public async ValueTask<IActionResult> AssignOrganizations(int id, IEnumerable<int> organizationsIds)
  {
    try
    {
      var errorOrUser =
        await userService.RetrieveUserByIdWithDetailsAsync(id, false, [nameof(Domain.Entities.User.UserOrganizations)]);

      if (errorOrUser.IsError)
      {
        return Problem(errorOrUser.Errors);
      }

      User user = errorOrUser.Value;

      if (user.UserOrganizations is { Count: > 0 })
      {
        organizationsIds =
          organizationsIds.Where(id => !user.UserOrganizations.Select(uO => uO.OrganizationId).Contains(id));
      }

      if (!organizationsIds.Any())
      {
        return Problem([UserError.AlreadyAssignedToOrganizations]);
      }

      var assignedOrganizations = (await userOrganizationService.AddMultipleUserOrganizationsAsync(
        organizationsIds.Select(organizationId => new UserOrganization()
        { UserId = id, OrganizationId = organizationId }))).ToList();

      return Ok(mapper.Map<List<UserOrganizationDto>>(assignedOrganizations));
    }
    catch (DbUpdateException ex)
      when (ex.InnerException is Npgsql.PostgresException inner && inner.SqlState == "23503")
    {
      return Problem([Error.NotFound("AssignedOrganizations.NotFound", description: "Some of assigned organizations not found in database")]);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }

  [Authorize(Roles = Role.SuperAdmin)]
  [HttpDelete("{userId:int:required}/unassign-organization/{organizationId}")]
  public async ValueTask<IActionResult> UnAssingOrganization(int userId, int organizationId)
  {
    try
    {
      var errorOrAssignedOrganization =
        await userOrganizationService.RetrieveByUserIdAndOrganizationIdAsync(userId, organizationId);

      if (errorOrAssignedOrganization.IsError)
      {
        return Problem(errorOrAssignedOrganization.Errors);
      }

      var errorOrDeletedUserOrganization =
        await userOrganizationService.RemoveAsync(errorOrAssignedOrganization.Value);

      return errorOrAssignedOrganization.Match(
        value => NoContent(),
        Problem);
    }
    catch
    {
      return Problem([Error.Unexpected()]);
    }
  }
}
