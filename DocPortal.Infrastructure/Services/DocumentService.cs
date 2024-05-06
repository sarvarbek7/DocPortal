using System.Linq.Expressions;

using DocPortal.Application.Options;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;
using DocPortal.Infrastructure.Common.Extensions;
using DocPortal.Infrastructure.Services.Bases;
using DocPortal.Persistance.Repositories.Interfaces;

using ErrorOr;

using FluentValidation;

using Microsoft.EntityFrameworkCore;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Infrastructure.Services;

internal class DocumentService(IDocumentRepository repository, IValidator<Document> validator) :
  CrudService<Document, Guid>(repository, validator), IDocumentService
{
  public new async ValueTask<ErrorOr<Document>> AddEntityAsync(Document entity,
                                                           bool saveChanges = true,
                                                           CancellationToken cancellationToken = default)
    => await base.AddEntityAsync(entity, saveChanges, cancellationToken);
  public async ValueTask<IEnumerable<Document>> AddMultipleDocuments(IEnumerable<Document> documents,
                                                                     bool saveChanges = true,
                                                                     CancellationToken cancellationToken = default)
  {
    return await repository.AddEntitiesRangeAsync(documents, saveChanges, cancellationToken);
  }
  public async ValueTask<ErrorOr<Document>> RetrieveDocumentByIdWithDetailsAsync(Guid id,
                                                                           bool asNoTracking = false,
                                                                           CancellationToken cancellationToken = default,
                                                                           ICollection<string>? includedNavigationalProperties = null)
  {
    try
    {
      var document =
        await repository.GetEntities(document => document.Id == id, asNoTracking)
        .ApplyIncludedNavigations(includedNavigationalProperties).FirstOrDefaultAsync();

      return document is null ? DocumentError.NotFound : document;
    }
    catch (Exception ex)
    {
      return Error.Unexpected();
    }

    throw new NotImplementedException();
  }

  public new async ValueTask<ErrorOr<Document>> ModifyAsync(Document entity,
                                                  bool saveChanges = true,
                                                  CancellationToken cancellationToken = default)
    => await base.ModifyAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<Document>> RemoveAsync(Document entity,
                                                      bool saveChanges = true,
                                                      CancellationToken cancellationToken = default)
    => await base.RemoveAsync(entity, saveChanges, cancellationToken);

  public new async ValueTask<ErrorOr<Document>> RemoveByIdAsync(Guid id,
                                                          bool saveChanges = true,
                                                          CancellationToken cancellationToken = default,
                                                          int? deletedBy = null)
    => await base.RemoveByIdAsync(id, saveChanges, cancellationToken, deletedBy);

  public new IEnumerable<Document> RetrieveAll(PageOptions pageOptions,
                                                    Expression<Func<Document, bool>>? predicate = null,
                                                    bool asNoTracking = false,
                                                    ICollection<string>? includedNavigationalProperties = null,
                                                    Func<IQueryable<Document>, IOrderedQueryable<Document>>? orderFunc = null)
    => base.RetrieveAll(pageOptions, predicate, asNoTracking, includedNavigationalProperties, orderFunc);

  public new async ValueTask<ErrorOr<Document?>> RetrieveByIdAsync(Guid id,
                                                       CancellationToken cancellationToken = default)
    => await base.RetrieveByIdAsync(id, cancellationToken);
}
