using DocPortal.Contracts.Endpoints.Documents.Options;

namespace DocPortal.Contracts.Endpoints.Documents;
public record GetAllDocumentsRequest(PaginationOptions? PaginationOptions,
                                     DocumentFilterOptions? DocumentFilterOptions,
                                     DocumentIncludeQueryOptions? DocumentIncludeQueryOptions);
