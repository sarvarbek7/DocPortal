using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Auth;

public record LoginResponce(UserDto User,
                            string Login,
                            string Role,
                            string Token,
                            DateTime ExpireAt,
                            ICollection<UserOrganizationDto>? UserOrganizations = null);
