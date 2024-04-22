using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class User : Entity<int, int>
{
  public string PhysicalIdentity { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string JobPosition { get; set; }
  public string? Login { get; set; }
  public string Role { get; set; } = Common.Role.User;
  public string? PasswordHash { get; set; }
  public ICollection<UserOrganization>? UserOrganizations { get; set; }
}
