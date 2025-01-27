﻿using System.Linq.Expressions;

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
                                                         CancellationToken cancellationToken = default)
  {
    return await this.DbContext.Set<TEntity>().FindAsync(keyValues: [id], cancellationToken);
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
  ///// <param name="entity"></param>
  /// <param name="saveChanges"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  protected async ValueTask<TEntity> DeleteEntityAsync(TEntity entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
  {
    DbContext.Set<TEntity>().Remove(entity);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync(cancellationToken);
    }

    return entity;
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
    DbContext.Set<TEntity>().RemoveRange(entities);

    if (saveChanges)
    {
      await DbContext.SaveChangesAsync();
    }

    return entities;
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    return await DbContext.SaveChangesAsync(cancellationToken);
  }
}
