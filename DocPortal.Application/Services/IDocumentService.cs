using DocPortal.Application.Services.Bases;
using DocPortal.Domain.Entities;

namespace DocPortal.Application.Services;

public interface IDocumentService : ICRUDService<Document, Guid>
{
}
