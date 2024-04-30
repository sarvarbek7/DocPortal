using System.Linq.Expressions;

using DocPortal.Contracts.Endpoints;
using DocPortal.Domain.Common.Entities;

namespace DocPortal.Api.QueryServices
{
  public interface IQueryService<TEntity> where TEntity : class, IEntity
  {
    Expression<Func<TEntity, bool>>? ApplyFilterOptions(IFilterOptions<TEntity>? filterOptions);
    ICollection<string> ApplyIncludeQueries(IIncludeQueryOptions<TEntity>? includeQueryOptions);
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? ApplyOrderbyQuery(string? orderby, bool isDescending = false);
  }
}
