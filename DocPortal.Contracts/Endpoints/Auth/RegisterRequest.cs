using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Auth;

public record RegisterRequest(UserDto User,
                              string Login,
                              string Password,
                              string? Role,
                              ICollection<int>? UserOrganizations = null);
