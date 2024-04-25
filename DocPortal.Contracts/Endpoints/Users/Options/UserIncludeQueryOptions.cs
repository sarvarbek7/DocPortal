using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Users.Options;

public record UserIncludeQueryOptions(bool IncludeUserOrganizations = false, bool IncludeLogin = false)
  : IIncludeQueryOptions<User>;
