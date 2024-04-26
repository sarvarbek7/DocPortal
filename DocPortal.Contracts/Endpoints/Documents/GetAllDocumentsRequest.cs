using DocPortal.Contracts.Endpoints.Documents.Options;

namespace DocPortal.Contracts.Endpoints.Documents;
public record GetAllDocumentsRequest(DocumentFilterOptions? DocumentFilterOptions,
                                     DocumentIncludeQueryOptions? DocumentIncludeQueryOptions);
