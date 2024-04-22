using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

namespace DocPortal.Persistance.Repositories;

internal class UserRepository(ApplicationDbContext context) :
  EntityRepositoryBase<ApplicationDbContext, User, int>((context)),
  IUserRepository
{
  public new ValueTask<IEnumerable<User>> AddEntitiesRangeAsync(IEnumerable<User> entities,
                                                                bool saveChanges = true,
                                                                CancellationToken cancellationToken = default)
    => base.AddEntitiesRangeAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<User> AddEntityAsync(User entity,
                                            bool saveChanges = true,
                                            CancellationToken cancellationToken = default)
    => base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<User>> DeleteEntitiesAsync(IEnumerable<User> entities,
                                                              bool saveChanges = true,
                                                              CancellationToken cancellationToken = default)
    => base.DeleteEntitiesAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<User>> DeleteEntitiesByIdsAsync(IEnumerable<int> ids,
                                                                   bool saveChanges = true,
                                                                   CancellationToken cancellationToken = default)
    => base.DeleteEntitiesByIdsAsync(ids, saveChanges, cancellationToken);

  public new ValueTask<User> DeleteEntityAsync(User entity,
                                               bool saveChanges = true,
                                               CancellationToken cancellationToken = default)
    => base.DeleteEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<User> DeleteEntityByIdAsync(int id,
                                                   bool saveChanges = true,
                                                   CancellationToken cancellationToken = default)
    => base.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

  public new IQueryable<User> GetEntities(Expression<Func<User, bool>>? predicate = null,
                                          bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<User?> GetEntityByIdAsync(int id,
                                                 bool asNoTracking = false,
                                                 CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

  public new ValueTask<User> UpdateAsync(User entity,
                                         bool saveChanges = true,
                                         CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
