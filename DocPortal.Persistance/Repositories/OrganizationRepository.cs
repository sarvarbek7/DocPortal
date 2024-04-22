using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

namespace DocPortal.Persistance.Repositories;

internal class OrganizationRepository(ApplicationDbContext context) :
  EntityRepositoryBase<ApplicationDbContext, Organization, int>(context),
  IOrganizationRepository
{
  public new ValueTask<IEnumerable<Organization>> AddEntitiesRangeAsync(IEnumerable<Organization> entities,
                                                                        bool saveChanges = true,
                                                                        CancellationToken cancellationToken = default)
    => base.AddEntitiesRangeAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<Organization> AddEntityAsync(Organization entity,
                                                    bool saveChanges = true,
                                                    CancellationToken cancellationToken = default)
    => base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<Organization>> DeleteEntitiesAsync(IEnumerable<Organization> entities,
                                                                      bool saveChanges = true,
                                                                      CancellationToken cancellationToken = default)
    => base.DeleteEntitiesAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<Organization>> DeleteEntitiesByIdsAsync(IEnumerable<int> ids,
                                                                           bool saveChanges = true,
                                                                           CancellationToken cancellationToken = default)
    => base.DeleteEntitiesByIdsAsync(ids, saveChanges, cancellationToken);

  public new ValueTask<Organization> DeleteEntityAsync(Organization entity,
                                                       bool saveChanges = true,
                                                       CancellationToken cancellationToken = default)
    => base.DeleteEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<Organization> DeleteEntityByIdAsync(int id,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => base.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

  public new IQueryable<Organization> GetEntities(Expression<Func<Organization, bool>>? predicate = null,
                                                  bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<Organization?> GetEntityByIdAsync(int id,
                                                         bool asNoTracking = false,
                                                         CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

  public new ValueTask<Organization> UpdateAsync(Organization entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
