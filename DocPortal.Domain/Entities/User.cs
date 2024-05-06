using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class User : Entity<int, int>
{
  private string _role = Common.Role.User.ToLower();

  public string PhysicalIdentity { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string JobPosition { get; set; }
  public string Role
  {
    get => _role; set
    {
      if (value is not null && Common.Role.AvailableRoles.Contains(value.ToLower()))
      {
        _role = value.ToLower();
      }
    }
  }
  public UserCredential? UserCredential { get; set; }
  public ICollection<UserOrganization>? UserOrganizations { get; set; }

  public override void UpdateEntityState(IEntity basedOnThisEntity)
  {
    if (basedOnThisEntity is not User)
    {
      throw new InvalidOperationException();
    }
    else
    {
      User user = (basedOnThisEntity as User)!;

      this.PhysicalIdentity = user.PhysicalIdentity;
      this.FirstName = user.FirstName;
      this.LastName = user.LastName;
      this.JobPosition = user.JobPosition;
      this.Role = user.Role;
      this.UpdatedBy = user.UpdatedBy;
    }
  }
}
