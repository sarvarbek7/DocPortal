using DocPortal.Domain.Entities;
using DocPortal.Persistance.Repositories.Interfaces.Bases;

namespace DocPortal.Persistance.Repositories.Interfaces;

public interface IDocumentTypeRepository : IBasicCrudRepository<DocumentType, int>
{ }
