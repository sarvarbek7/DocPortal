using System.Linq.Expressions;

using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Domain.Options;
using DocPortal.Infrastructure.Service.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

namespace DocPortal.Infrastructure.Service;

internal class DocumentService(IDocumentRepository repository, IValidator<Document> validator) :
  CRUDService<Document, Guid>(repository, validator), IDocumentService
{
  public async ValueTask<ErrorOr<Document>> AddEntityAsync(Document entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Document>> ModifyAsync(Document entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Document>> RemoveAsync(Document entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public async ValueTask<ErrorOr<Document>> RemoveByIdAsync(Guid id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken);

  public IEnumerable<Document> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<Document, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties);

  public async ValueTask<ErrorOr<Document?>> RetrieveByIdAsync(Guid id,
                                                       bool asNoTracking = false,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, asNoTracking, cancellationToken);
}
