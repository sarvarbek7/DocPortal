using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Documents;

public record GetAllDocumentsResponse(IEnumerable<DocumentDto> Documents,
                                      int Total,
                                      int PageSize);
