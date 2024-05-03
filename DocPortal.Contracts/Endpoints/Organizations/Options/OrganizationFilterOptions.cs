using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Organizations.Options;

public record OrganizationFilterOptions(
  string? Keyword,
  int? ParentId) : IFilterOptions<Organization>;

