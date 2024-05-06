using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Application.Services.Processing;
using DocPortal.Domain.Common.Entities;
using DocPortal.Persistance.DataContext;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Services.Processing;

internal class DeletedEntitiesService(ApplicationDbContext context) : IDeletedEntitesService
{
  public IEnumerable<TEntity> RetrieveDeletedEntities<TEntity>(PageOptions pageOptions,
                                                               Expression<Func<TEntity, bool>>? predicate = null,
                                                               bool asNoTracking = false,
                                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
                                                               bool? ignorePagination = false)
    where TEntity : class, ISoftDeletedEntity
  {
    try
    {

      var initialQuery = context.Set<TEntity>().AsQueryable().IgnoreQueryFilters()
        .Where(entity => entity.IsDeleted);

      if (asNoTracking)
      {
        initialQuery = initialQuery.AsNoTracking();
      }

      if (predicate is not null)
      {
        initialQuery = initialQuery.Where(predicate);
      }

      if (orderFunc is not null)
      {
        initialQuery = orderFunc(initialQuery);
      }

      pageOptions ??= new PageOptions(null, null);

      initialQuery = initialQuery
        .Skip((pageOptions.PageToken - 1) * pageOptions.PageSize)
        .Take(pageOptions.PageSize);

      return initialQuery.AsEnumerable();
    }
    catch
    {
      throw;
    }
  }

  public async Task RestoreEntity<TEntity, TAuditId>(TEntity entity)
    where TEntity : class, ISoftDeteledEntity<TAuditId>
    where TAuditId : struct
  {
    entity.Undo();
    entity.DeletedBy = null;

    var entry = context.Entry(entity);
    entry.Property(entity => entity.IsDeleted).IsModified = true;
    entry.Property(entity => entity.DeletedAt).IsModified = true;
    entry.Property(entity => entity.DeletedBy).IsModified = true;

    if (entity is IAuditableEntity<TAuditId>)
    {
      entry.Property(entity =>
      (entity as IAuditableEntity<TAuditId>)!.UpdatedBy).IsModified = true;
    }

    await context.SaveChangesAsync();
  }
}
