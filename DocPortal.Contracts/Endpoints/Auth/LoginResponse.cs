using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Auth;

public record LoginResponse(UserDto User,
                            string Login,
                            string Role,
                            string Token,
                            DateTime ExpireAt);
