using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Domain.Common.Entities;

using ErrorOr;

namespace DocPortal.Application.Services.Bases;

public interface ICrudService<TEntity, TId>
  where TEntity : class, IEntity<TId>
  where TId : struct
{
  IEnumerable<TEntity> RetrieveAll(PageOptions pageOptions,
                                   Expression<Func<TEntity, bool>>? predicate = null,
                                   bool asNoTracking = false,
                                   ICollection<string>? includedNavigationalProperties = null,
                                   Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
                                   bool ignorePagination = false);

  ValueTask<ErrorOr<TEntity?>> RetrieveByIdAsync(TId id,
                                         CancellationToken cancellationToken = default);

  ValueTask<ErrorOr<TEntity>> AddEntityAsync(TEntity entity,
                                    bool saveChanges = true,
                                    CancellationToken cancellationToken = default);

  ValueTask<ErrorOr<TEntity>> ModifyAsync(TEntity entity,
                                 bool saveChanges = true,
                                 CancellationToken cancellationToken = default);

  ValueTask<ErrorOr<TEntity>> RemoveAsync(TEntity entity,
                                       bool saveChanges = true,
                                       CancellationToken cancellationToken = default);

  ValueTask<ErrorOr<TEntity>> RemoveByIdAsync(TId id,
                                bool saveChanges = true,
                                CancellationToken cancellationToken = default,
                                int? deletedBy = null);

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
