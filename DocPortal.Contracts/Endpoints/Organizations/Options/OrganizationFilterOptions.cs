using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Organizations.Options;

public record OrganizationFilterOptions(
  string? Title,
  int? ParentId) : IFilterOptions<Organization>;

