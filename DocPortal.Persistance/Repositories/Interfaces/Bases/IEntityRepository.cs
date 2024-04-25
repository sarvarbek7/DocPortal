using System.Linq.Expressions;

using DocPortal.Domain.Common.Entities;

namespace DocPortal.Persistance.Repositories.Interfaces.Bases
{
  public interface IEntityRepository<TEntity, TId>
      where TEntity : class, IEntity<TId>
      where TId : struct
  {
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    ValueTask<bool> EntityExistsAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
  }
}
