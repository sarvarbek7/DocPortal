using DocPortal.Domain.Common.Entities;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Common.Extensions;

internal static class QueryExtentions
{
  public static IQueryable<TEntity> ApplyIncludedNavigations<TEntity>(this IQueryable<TEntity> query,
                                                                      ICollection<string>? includedNavigationalProperties = null)
    where TEntity : class, IEntity
  {
    if (includedNavigationalProperties is not null && includedNavigationalProperties.Count > 0)
    {
      foreach (string navigationalPropery in includedNavigationalProperties)
      {
        query = query.Include(navigationalPropery);
      }
    }

    return query;
  }
}
