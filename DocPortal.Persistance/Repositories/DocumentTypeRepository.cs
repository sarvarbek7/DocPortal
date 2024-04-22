using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

namespace DocPortal.Persistance.Repositories;

internal class DocumentTypeRepository(ApplicationDbContext context) :
  EntityRepositoryBase<ApplicationDbContext, DocumentType, int>(context),
  IDocumentTypeRepository
{
  public new ValueTask<DocumentType> AddEntityAsync(DocumentType entity,
                                                    bool saveChanges = true,
                                                    CancellationToken cancellationToken = default)
    => base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<DocumentType> DeleteEntityAsync(DocumentType entity,
                                                       bool saveChanges = true,
                                                       CancellationToken cancellationToken = default)
    => base.DeleteEntityAsync(entity, saveChanges, cancellationToken);

  public new ValueTask<DocumentType> DeleteEntityByIdAsync(int id,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => base.DeleteEntityByIdAsync(id, saveChanges, cancellationToken);

  public new IQueryable<DocumentType> GetEntities(Expression<Func<DocumentType, bool>>? predicate = null,
                                                  bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<DocumentType?> GetEntityByIdAsync(int id,
                                                         bool asNoTracking = false,
                                                         CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, asNoTracking, cancellationToken);

  public new ValueTask<DocumentType> UpdateAsync(DocumentType entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
