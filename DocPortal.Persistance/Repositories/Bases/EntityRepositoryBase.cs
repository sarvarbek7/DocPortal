using System.Linq.Expressions;

using DocPortal.Domain.Common.Entities;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Persistance.Repositories.Bases;

internal abstract class EntityRepositoryBase<TContext, TEntity, TId>(TContext context)
  where TContext : DbContext
  where TEntity : class, IEntity<TId>
  where TId : struct
{
  protected TContext DbContext => context;

  /// <summary>
  /// Retrieving entities from the database based on optional filtering conditions, tracking preferences.
  /// </summary>
  /// <param name="predicate">Optinal filtering options</param>
  /// <param name="asNoTracking">Tracking behaviour</param>
  /// <returns>IQueryable collection of entities</returns>
  protected IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>>? predicate = null,
                                            bool asNoTracking = false)
  {
    var initialQuery = DbContext.Set<TEntity>().AsQueryable();

    if (asNoTracking)
    {
      initialQuery = initialQuery.AsNoTracking();
    }

    if (predicate is not null)
    {
      initialQuery = initialQuery.Where(predicate);
    }

    return initialQuery;
  }

  /// <summary>
  /// Retrieve entity from the database by id based on tracking behaviours and included navigational properties
  /// </summary>
  /// <param name="id"></param>
  /// <param name="asNoTracking"></param>
  /// <param name="includedProperties">List of navigational properties paths which are included with entity</param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<TEntity?> GetEntityByIdAsync(TId id,
                                                         bool asNoTracking = false,
                                                         CancellationToken cancellationToken = default)
  {
    var foundEntity = default(TEntity?);

    var initialQuery = DbContext.Set<TEntity>().AsQueryable();

    if (asNoTracking)
    {
      initialQuery = initialQuery.AsNoTracking();
    }

    foundEntity =
      await initialQuery.FirstOrDefaultAsync(
        entity => entity.Id!.Equals(id), cancellationToken);

    return foundEntity;
  }

  /// <summary>
  /// Add given entity
  /// </summary>
  /// <param name="entity"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<TEntity> AddEntityAsync(TEntity entity,
                                                    bool saveChanges = true,
                                                    CancellationToken cancellationToken = default)
  {
    await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    return entity;
  }

  /// <summary>
  /// Add multiple entities
  /// </summary>
  /// <param name="entities"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<IEnumerable<TEntity>> AddEntitiesRangeAsync(IEnumerable<TEntity> entities,
                                                                        bool saveChanges = true,
                                                                        CancellationToken cancellationToken = default)
  {
    await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    return entities;
  }

  /// <summary>
  /// Update entity
  /// </summary>
  /// <param name="entity"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<TEntity> UpdateAsync(TEntity entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
  {
    DbContext.Set<TEntity>().Update(entity);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    return entity;
  }

  /// <summary>
  /// Delete entity
  /// </summary>
  /// <param name="entity"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  protected async ValueTask<TEntity> DeleteEntityAsync(TEntity entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
  {
    if (entity is null)
    {
      throw new InvalidOperationException("Entity can not null");
    }

    DbContext.Set<TEntity>().Remove(entity);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    return entity;
  }

  /// <summary>
  /// Delete entity by id
  /// </summary>
  /// <param name="id"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<TEntity> DeleteEntityByIdAsync(TId id,
                                                     bool saveChanges = true,
                                                     CancellationToken cancellationToken = default)
  {
    var foundEntity = await GetEntityByIdAsync(id: id, cancellationToken: cancellationToken);

    return await DeleteEntityAsync(foundEntity, saveChanges, cancellationToken);
  }

  /// <summary>
  /// Delete multiple entities
  /// </summary>
  /// <param name="entities"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<IEnumerable<TEntity>> DeleteEntitiesAsync(IEnumerable<TEntity> entities,
                                                                           bool saveChanges = true,
                                                                           CancellationToken cancellationToken = default)
  {
    if (entities is null)
    {
      throw new InvalidOperationException("Entities can not be null");
    }
    DbContext.Set<TEntity>().RemoveRange(entities);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync();
    }

    return entities;
  }

  /// <summary>
  /// Delete multiple entities by ids
  /// </summary>
  /// <param name="ids"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  protected async ValueTask<IEnumerable<TEntity>> DeleteEntitiesByIdsAsync(IEnumerable<TId> ids,
                                                                           bool saveChanges = true,
                                                                           CancellationToken cancellationToken = default)
  {
    Expression<Func<TEntity, bool>> findByIds =
      (entity) => ids.Contains(entity.Id);

    IQueryable<TEntity>? foundEntities = GetEntities(findByIds);

    return await DeleteEntitiesAsync(foundEntities, saveChanges, cancellationToken);
  }

  public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
  {
    return await DbContext.SaveChangesAsync(cancellationToken);
  }
}
