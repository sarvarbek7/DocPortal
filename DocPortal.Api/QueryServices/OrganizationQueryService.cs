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

    string? keyword = filter?.Keyword?.ToLower();
    int? parentId = filter.ParentId;


    if (filterOptions is null)
    {
      return predicate;
    }

    predicate = (org) =>
       (
        (keyword == null || org.Title.ToLower().Contains(keyword)) ||
        (keyword == null || org.PhysicalIdentity.ToLower().Contains(keyword))
       )
       &&
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

  public Func<IQueryable<Organization>, IOrderedQueryable<Organization>>? ApplyOrderbyQuery(string? orderby, bool isDescending = false)
  {
    if (orderby is null)
    {
      return null;
    }

    return (orderby, isDescending) switch
    {
      ("title", false) => q => q.OrderBy(organization => organization.Title),
      ("title", true) => q => q.OrderByDescending(organization => organization.Title),
      ("physicalIdentity", false) => q => q.OrderBy(organization => organization.PhysicalIdentity),
      ("physicalIdentity", true) => q => q.OrderByDescending(organization => organization.PhysicalIdentity),
      _ => null
    };
  }
}
