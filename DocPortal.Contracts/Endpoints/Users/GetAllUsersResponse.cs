using DocPortal.Contracts.Dtos;

namespace DocPortal.Contracts.Endpoints.Users;

public record GetAllUsersResponse(IEnumerable<UserDto> Users,
                                  int Total,
                                  int PageSize);
