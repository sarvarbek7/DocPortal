using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Organizations.Options;

public record OrganizationIncludeQueryOptions(
  bool IncludeAdmins = false,
  bool IncludeDocuments = false,
  bool IncludeSubordinates = false) : IIncludeQueryOptions<Organization>;
