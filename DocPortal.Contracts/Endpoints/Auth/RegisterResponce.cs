using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Auth;

public record RegisterResponce(UserDto User,
                               string Login,
                               string Role,
                               ICollection<UserOrganizationDto>? UserOrganizations = null);
