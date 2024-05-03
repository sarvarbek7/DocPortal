using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Users.Options;

public record UserFilterOptions(string? Keyword) : IFilterOptions<User>;
