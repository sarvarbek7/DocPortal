using System.Linq.Expressions;

using DocPortal.Contracts.Endpoints;
using DocPortal.Contracts.Endpoints.Users.Options;
using DocPortal.Domain.Entities;

namespace DocPortal.Api.QueryServices;

internal class UserQueryService : IQueryService<User>
{
  public Expression<Func<User, bool>>? ApplyFilterOptions(IFilterOptions<User>? filterOptions)
  {
    var filter = filterOptions as UserFilterOptions;

    string? keyword = filter?.Keyword?.ToLower();

    Expression<Func<User, bool>>? predicate = null;

    if (filter is null)
    {
      return predicate;
    }

    predicate = (user) => (
      (keyword == null || user.FirstName.ToLower().Contains(keyword)) ||
      (keyword == null || user.LastName.ToLower().Contains(keyword)) ||
      (keyword == null || user.PhysicalIdentity.ToLower().Contains(keyword)) ||
      (keyword == null || user.JobPosition.ToLower().Contains(keyword)) ||
      (keyword == null || user.Role.ToLower().Contains(keyword))) &&
      (filter.isDeleted == null || user.IsDeleted);

    return predicate;
  }

  public ICollection<string> ApplyIncludeQueries(IIncludeQueryOptions<User>? includeQueryOptions)
  {
    var includeQuery = includeQueryOptions as UserIncludeQueryOptions;

    ICollection<string>? includedNavigationalProperties = [];

    if (includeQuery is not null)
    {
      if (includeQuery.IncludeUserOrganizations)
      {
        includedNavigationalProperties.Add(
          $"{nameof(User.UserOrganizations)}." +
          $"{nameof(UserOrganization.AssignedOrganization)}");
      }

      if (includeQuery.IncludeLogin)
      {
        includedNavigationalProperties.Add(nameof(User.UserCredential));
      }
    }

    return includedNavigationalProperties;
  }

  public Func<IQueryable<User>, IOrderedQueryable<User>>? ApplyOrderbyQuery(string? orderby, bool isDescending = false)
  {
    if (orderby is null)
    {
      return null;
    }

    return (orderby, isDescending) switch
    {
      ("firstName", false) => q => q.OrderBy(user => user.FirstName),
      ("firstName", true) => q => q.OrderByDescending(user => user.FirstName),
      ("lastName", false) => q => q.OrderBy(user => user.LastName),
      ("lastName", true) => q => q.OrderByDescending(user => user.LastName),
      _ => null
    };
  }
}
