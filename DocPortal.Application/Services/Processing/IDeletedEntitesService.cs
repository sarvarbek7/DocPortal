using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Domain.Common.Entities;

namespace DocPortal.Application.Services.Processing;

public interface IDeletedEntitesService
{
  Task RestoreEntity<TEntity, TAuditId>(TEntity entity)
    where TEntity : class, ISoftDeteledEntity<TAuditId>
    where TAuditId : struct;
  IEnumerable<TEntity> RetrieveDeletedEntities<TEntity>(PageOptions pageOptions,
                                                        Expression<Func<TEntity, bool>>? predicate = null,
                                                        bool asNoTracking = false,
                                                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderFunc = null,
                                                        bool? ignorePagination = false)
    where TEntity : class, ISoftDeletedEntity;
}
