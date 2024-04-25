using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Documents.Options;

public record DocumentIncludeQueryOptions(bool IncludeDocumentType = false,
                                          bool IncludeOrganization = false) : IIncludeQueryOptions<Document>;
