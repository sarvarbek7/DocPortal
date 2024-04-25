using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

using ErrorOr;

namespace DocPortal.Application.Services;

public interface IDocumentService : ICrudService<Document, Guid>
{
  ValueTask<ErrorOr<Document>> RetrieveDocumentByIdWithDetailsAsync(Guid id,
                                                                    bool asNoTracking = false,
                                                                    CancellationToken cancellationToken = default,
                                                                    ICollection<string>? includedNavigationalProperties = null);
  ValueTask<IEnumerable<Document>> AddMultipleDocuments(IEnumerable<Document> documents, bool saveChanges = true, CancellationToken cancellationToken = default);
}
