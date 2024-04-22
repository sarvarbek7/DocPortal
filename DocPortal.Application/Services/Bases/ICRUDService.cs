using System.Linq.Expressions;

using DocPortal.Domain.Common.Entities;
using DocPortal.Domain.Options;

using ErrorOr;

namespace DocPortal.Application.Services.Bases;

public interface ICRUDService<TEntity, TId>
  where TEntity : class, IEntity<TId>
  where TId : struct
{
  IEnumerable<TEntity> RetrieveAll(PageOptions pageOptions,
                                   Expression<Func<TEntity, bool>>? predicate = null,
                                   bool asNoTracking = false,
                                   ICollection<string>? includedNavigationalProperties = null);

  ValueTask<ErrorOr<TEntity?>> RetrieveByIdAsync(TId id,
                                         bool asNoTracking = false,
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
                                CancellationToken cancellationToken = default);

  Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
