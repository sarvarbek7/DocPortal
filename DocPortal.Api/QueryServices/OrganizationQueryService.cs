using System.Linq.Expressions;

using DocPortal.Contracts.Endpoints;
using DocPortal.Contracts.Endpoints.Organizations.Options;
using DocPortal.Domain.Entities;

namespace DocPortal.Api.QueryServices;

internal class OrganizationQueryService : IQueryService<Organization>
{
  public Expression<Func<Organization, bool>>? ApplyFilterOptions(IFilterOptions<Organization>? filterOptions)
  {
    var filter = filterOptions as OrganizationFilterOptions;

    Expression<Func<Organization, bool>>? predicate = null;

    string? title = filter?.Title?.ToLower();
    int? parentId = filter?.ParentId;


    if (filterOptions is null)
    {
      return predicate;
    }

    predicate = (org) =>
       (title == null || org.Title.ToLower().Contains(title)) &&
       (parentId == null || org.PrimaryOrganizationId == parentId);

    return predicate;
  }

  public ICollection<string> ApplyIncludeQueries(IIncludeQueryOptions<Organization>? includeQueryOptions)
  {
    var includedQuery =
      includeQueryOptions as OrganizationIncludeQueryOptions;

    ICollection<string>? includedNavigationalProperties = [nameof(Organization.PrimaryOrganization)];

    if (includeQueryOptions is not null)
    {
      if (includedQuery.IncludeAdmins)
      {
        includedNavigationalProperties.Add($"{nameof(Organization.Admins)}.{nameof(UserOrganization.Admin)}");
      }

      if (includedQuery.IncludeDocuments)
      {
        includedNavigationalProperties.Add(nameof(Organization.Documents));
      }
      if (includedQuery.IncludeSubordinates)
      {
        includedNavigationalProperties.Add(nameof(Organization.Subordinates));
      }
    }

    return includedNavigationalProperties;
  }
}
