using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Documents.Options;

public record DocumentFilterOptions(string? Title,
                                    string? RegisterNumber,
                                    int? OrganizationId,
                                    int? DocumentTypeId,
                                    DateOnly? StartDate,
                                    DateOnly? EndDate,
                                    bool? isDeleted = null) : IFilterOptions<Document>;
