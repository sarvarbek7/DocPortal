using System.Linq.Expressions;

using DocPortal.Domain.Common.Entities;

namespace DocPortal.Persistance.Repositories.Interfaces.Bases;

public interface IBasicCrudRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
  where TEntity : class, IEntity<TId>
  where TId : struct
{
  IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? predicate = null,
                                  bool asNoTracking = false);

  ValueTask<TEntity?> GetEntityByIdAsync(TId id,
                                         CancellationToken cancellationToken = default);

  ValueTask<TEntity> AddEntityAsync(TEntity entity,
                                    bool saveChanges = true,
                                    CancellationToken cancellationToken = default);

  ValueTask<TEntity> UpdateAsync(TEntity entity,
                                 bool saveChanges = true,
                                 CancellationToken cancellationToken = default);

  ValueTask<TEntity> DeleteEntityAsync(TEntity entity,
                                       bool saveChanges = true,
                                       CancellationToken cancellationToken = default);
}
