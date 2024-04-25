using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Users;

public record GetUserByIdResponse(UserDto User,
                                  string? Login,
                                  IEnumerable<UserOrganizationDto>? Organizations);
