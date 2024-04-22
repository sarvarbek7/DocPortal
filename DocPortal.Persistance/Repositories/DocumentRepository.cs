using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

namespace DocPortal.Persistance.Repositories;

internal class DocumentRepository(ApplicationDbContext context) :
  EntityRepositoryBase<ApplicationDbContext, Document, Guid>(context),
  IDocumentRepository
{
  public new ValueTask<IEnumerable<Document>> AddEntitiesRangeAsync(IEnumerable<Document> entities,
                                                                    bool saveChanges = true,
                                                                    CancellationToken cancellationToken = default)
    => base.AddEntitiesRangeAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<Document> AddEntityAsync(Document entity,
                                                bool saveChanges = true,
                                                CancellationToken cancellationToken = default)
    => base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<Document>> DeleteEntitiesAsync(IEnumerable<Document> entities,
                                                                  bool saveChanges = true,
                                                                  CancellationToken cancellationToken = default)
    => base.DeleteEntitiesAsync(entities, saveChanges, cancellationToken);

  public new ValueTask<IEnumerable<Document>> DeleteEntitiesByIdsAsync(IEnumerable<Guid> ids,
                                                                       bool saveChanges = true,
                                                                       CancellationToken cancellationToken = default)
    => base.DeleteEntitiesByIdsAsync(ids, saveChanges, cancellationToken);

  public new ValueTask<Document> DeleteEntityAsync(Document entity,
                                                   bool saveChanges = true,
                                                   CancellationToken cancellationToken = default)
    => base.DeleteEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<Document> DeleteEntityByIdAsync(Guid id,
                                                       bool saveChanges = true,
                                                       CancellationToken cancellationToken = default)
    => base.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

  public new IQueryable<Document> GetEntities(Expression<Func<Document, bool>>? predicate = null,
                                              bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<Document?> GetEntityByIdAsync(Guid id,
                                                     bool asNoTracking = false,
                                                     CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

  public new ValueTask<Document> UpdateAsync(Document entity,
                                             bool saveChanges = true,
                                             CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
