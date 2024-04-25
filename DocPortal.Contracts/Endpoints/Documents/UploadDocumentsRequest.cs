using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Documents
{
  public record UploadDocumentsRequest(int UserId,
                                       int OrganizationId,
                                       List<DocumentDto> Documents);
}
