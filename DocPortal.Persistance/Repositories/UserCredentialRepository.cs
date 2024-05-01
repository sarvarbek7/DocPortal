using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Persistance.Repositories;

internal class UserCredentialRepository(ApplicationDbContext dbContext) :
  EntityRepositoryBase<ApplicationDbContext, UserCredential, int>(dbContext), IUserCredentialsRepository
{
  public new async ValueTask<UserCredential> AddEntityAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default) =>
    await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<UserCredential> DeleteEntityAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.DeleteEntityAsync(entity);

  public async ValueTask<bool> EntityExistsAsync(Expression<Func<UserCredential, bool>>? predicate = null, CancellationToken cancellationToken = default)
  {
    return predicate is null
      ? await DbContext.Set<UserCredential>().AnyAsync(cancellationToken)
      : await DbContext.Set<UserCredential>().AnyAsync(predicate, cancellationToken);
  }

  public new IQueryable<UserCredential> GetEntities(Expression<Func<UserCredential, bool>>? predicate = null, bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new async ValueTask<UserCredential?> GetEntityByIdAsync(int id,
                                                                 CancellationToken cancellationToken = default)
    => await base.GetEntityByIdAsync(id, cancellationToken);

  public new async ValueTask<UserCredential> UpdateAsync(UserCredential entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    => await base.UpdateAsync(entity, saveChanges, cancellationToken);
}
