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

    string? firstname = filter?.Firstname?.ToLower();
    string? lastname = filter?.Lastname?.ToLower();
    string? jobPosition = filter?.JobPosition?.ToLower();
    string? role = filter?.Role?.ToLower();

    Expression<Func<User, bool>>? predicate = null;

    if (filter is null)
    {
      return predicate;
    }

    predicate = (user) =>
      (firstname == null || user.FirstName.ToLower().Contains(firstname)) &&
      (lastname == null || user.LastName.ToLower().Contains(lastname)) &&
      (jobPosition == null || user.JobPosition.ToLower().Contains(jobPosition)) &&
      (role == null || user.Role.ToLower().Contains(role));

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

  }
}
