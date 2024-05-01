using DocPortal.Domain.Common.Entities;

namespace DocPortal.Persistance.Repositories.Interfaces.Bases;

public interface IAdvancedCrudRepository<TEntity, TId> : IEntityRepository<TEntity, TId>
  where TEntity : class, IEntity<TId>
  where TId : struct
{
  ValueTask<IEnumerable<TEntity>> AddEntitiesRangeAsync(IEnumerable<TEntity> entities,
                                                        bool saveChanges = true,
                                                        CancellationToken cancellationToken = default);

  ValueTask<IEnumerable<TEntity>> DeleteEntitiesAsync(IEnumerable<TEntity> entities,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default);
}
