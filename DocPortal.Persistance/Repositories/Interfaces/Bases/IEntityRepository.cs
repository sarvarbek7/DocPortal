using DocPortal.Domain.Common.Entities;

namespace DocPortal.Persistance.Repositories.Interfaces.Bases
{
  public interface IEntityRepository<TEntity, TId>
      where TEntity : class, IEntity<TId>
      where TId : struct
  {
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
  }
}
