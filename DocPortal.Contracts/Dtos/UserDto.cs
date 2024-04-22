namespace DocPortal.Contracts.Dtos;

public record UserDto(int Id,
                      string FirstName,
                      string LastName,
                      string PhysicalIdentity,
                      string JobPosition);
