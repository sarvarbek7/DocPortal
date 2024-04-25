using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Persistance.Repositories;

internal class UserOrganizationRepository(ApplicationDbContext context) :
  EntityRepositoryBase<ApplicationDbContext, UserOrganization, int>(context),
  IUserOrganizationRepository
{
  public new ValueTask<IEnumerable<UserOrganization>> AddEntitiesRangeAsync(IEnumerable<UserOrganization> entities,
                                                                bool saveChanges = true,
                                                                CancellationToken cancellationToken = default)
    => base.AddEntitiesRangeAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<UserOrganization> AddEntityAsync(UserOrganization entity,
                                            bool saveChanges = true,
                                            CancellationToken cancellationToken = default)
    => base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<UserOrganization>> DeleteEntitiesAsync(IEnumerable<UserOrganization> entities,
                                                              bool saveChanges = true,
                                                              CancellationToken cancellationToken = default)
    => base.DeleteEntitiesAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<UserOrganization>> DeleteEntitiesByIdsAsync(IEnumerable<int> ids,
                                                                   bool saveChanges = true,
                                                                   CancellationToken cancellationToken = default)
    => base.DeleteEntitiesByIdsAsync(ids, saveChanges, cancellationToken);

  public new ValueTask<UserOrganization> DeleteEntityAsync(UserOrganization entity,
                                               bool saveChanges = true,
                                               CancellationToken cancellationToken = default)
    => base.DeleteEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<UserOrganization> DeleteEntityByIdAsync(int id,
                                                   bool saveChanges = true,
                                                   CancellationToken cancellationToken = default)
    => base.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

  public async ValueTask<bool> EntityExistsAsync(Expression<Func<UserOrganization, bool>>? predicate = null, CancellationToken cancellationToken = default)
  {
    return predicate is null
      ? await DbContext.Set<UserOrganization>().AnyAsync(cancellationToken)
      : await DbContext.Set<UserOrganization>().AnyAsync(predicate, cancellationToken);
  }

  public new IQueryable<UserOrganization> GetEntities(Expression<Func<UserOrganization, bool>>? predicate = null,
                                          bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<UserOrganization?> GetEntityByIdAsync(int id,
                                                 bool asNoTracking = false,
                                                 CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

  public new ValueTask<UserOrganization> UpdateAsync(UserOrganization entity,
                                         bool saveChanges = true,
                                         CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
