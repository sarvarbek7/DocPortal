using DocPortal.Domain.Entities;

namespace DocPortal.Contracts.Endpoints.Users.Options;

public record UserFilterOptions(string? Firstname,
                                string? Lastname,
                                string? JobPosition,
                                string? Role) : IFilterOptions<User>;
