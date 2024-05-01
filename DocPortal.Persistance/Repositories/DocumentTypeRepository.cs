using System.Linq.Expressions;

using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;
using DocPortal.Persistance.Repositories.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

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

  public async ValueTask<bool> EntityExistsAsync(Expression<Func<DocumentType, bool>>? predicate = null, CancellationToken cancellationToken = default)
  {
    return predicate is null
      ? await DbContext.Set<DocumentType>().AnyAsync(cancellationToken)
      : await DbContext.Set<DocumentType>().AnyAsync(predicate, cancellationToken);
  }

  public new IQueryable<DocumentType> GetEntities(Expression<Func<DocumentType, bool>>? predicate = null,
                                                  bool asNoTracking = false)
    => base.GetEntities(predicate, asNoTracking);

  public new ValueTask<DocumentType?> GetEntityByIdAsync(int id,
                                                         CancellationToken cancellationToken = default)
    => base.GetEntityByIdAsync(id, cancellationToken);

  public new ValueTask<DocumentType> UpdateAsync(DocumentType entity,
                                                 bool saveChanges = true,
                                                 CancellationToken cancellationToken = default)
    => base.UpdateAsync(entity, saveChanges, cancellationToken);
}
