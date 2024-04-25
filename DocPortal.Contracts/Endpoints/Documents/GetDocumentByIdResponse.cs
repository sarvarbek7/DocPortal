using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Documents;

public record GetDocumentByIdResponse(DocumentDto Document,
                                      DocumentTypeDto? DocumentType,
                                      OrganizationDto? Organization);
