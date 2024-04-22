using System.Linq.Expressions;

using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Options;
using DocPortal.Infrastructure.Service.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

namespace DocPortal.Infrastructure.Service;

internal class DocumentTypeService(IDocumentTypeRepository repository, IValidator<DocumentType> validator) :
  CRUDService<DocumentType, int>(repository, validator), IDocumentTypeService
{
  public async ValueTask<ErrorOr<DocumentType>> AddEntityAsync(DocumentType entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<DocumentType>> ModifyAsync(DocumentType entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<DocumentType>> RemoveAsync(DocumentType entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<DocumentType>> RemoveByIdAsync(int id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public IEnumerable<DocumentType> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<DocumentType, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public async ValueTask<ErrorOr<DocumentType?>> RetrieveByIdAsync(int id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);

}
