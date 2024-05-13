using DocPortal.Application.Services;
using DocPortal.Application.Services.Processing;
using DocPortal.Contracts.Endpoints.Statistics;
using DocPortal.Domain.Entities;

using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocPortal.Api.Controllers;

[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService,
                                  IDocumentTypeService documentTypeService,
                                  IOrganizationService organizationService) : _ApiController
{
  [AllowAnonymous]
  [HttpGet("annual")]
  public IActionResult GetDaily([FromQuery] int? year)
  {
    statisticsService.GetDailyDocumentCount(year ?? default);

    return Ok(statisticsService.GetMonthlyDocumentCount(year ?? default));
  }

  [AllowAnonymous]
  [HttpGet("general")]
  public IActionResult GetInitialStatistics()
  {
    try
    {
      int documentsCount = statisticsService.GetDocumentsCount(null);
      int organizationsCount = statisticsService.GetOrganizationsCount(null);
      int downloadsCount = statisticsService.GetDownloadsCount();

      return Ok(new GeneralStatisticsResponse(documentsCount,
                                              organizationsCount,
                                              downloadsCount));
    }
    catch
    {
      return Problem("Something went wrong.");
    }
  }

  [AllowAnonymous]
  [HttpGet("organizationStatistics")]
  public async ValueTask<IActionResult> GetOrganizationStatistics([FromQuery] int organizationId)
  {
    var documentTypes =
      documentTypeService.RetrieveAll(null, ignorePagination: true).ToList();

    ErrorOr<Organization> errorOrOrganization = await
      organizationService.RetrieveOrganizationByIdWithDetails(organizationId,
      includedNavigationalProperties: [nameof(Organization.Subordinates)]);

    if (errorOrOrganization.IsError)
    {
      return Problem(errorOrOrganization.Errors);
    }

    Organization organization = errorOrOrganization.Value;
    List<int> subs = statisticsService.GetSubordinates(organization.Id);

    List<int> checkedIds = [.. subs, organizationId];

    var docCountByOrgAndDocType =
      statisticsService.GetDocumentCountByOrgAndDoctype(
        doc => checkedIds.Contains(doc.OrganizationId));

    var getData = (Organization org) =>
    {
      List<int> subOfSubs = statisticsService.GetSubordinates(org.Id);
      List<int> subCheckedIds = [.. subOfSubs, org.Id];

      return new
      {
        Id = org.Id,
        Title = org.Title,
        HasSubordinate = subOfSubs.Any(),
        CountByDocType = documentTypes.Select(type => new
        {
          DocumentTypeId = type.Id,
          Count = docCountByOrgAndDocType.Where(dc =>
          dc.DocumentTypeId == type.Id &&
          subCheckedIds.Contains(dc.OrganizationId)).Sum(dc => dc.Count)
        })
      };
    };

    return Ok(new
    {
      Id = organization.Id,
      Title = organization.Title,
      HasSubordinate = subs.Any(),
      CountByDocType = documentTypes.Select(type => new
      {
        DocumentTypeId = type.Id,
        Count = docCountByOrgAndDocType.Where(dc =>
        dc.DocumentTypeId == type.Id &&
        checkedIds.Contains(dc.OrganizationId)).Sum(dc => dc.Count)
      }),
      Subordinates = organization.Subordinates?.Select(s => getData(s))
    });
  }
}
