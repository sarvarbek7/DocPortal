using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class UserOrganization : IEntity<int>
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int OrganizationId { get; set; }
  public User Admin { get; set; }
  public Organization AssignedOrganization { get; set; }

  public void UpdateEntityState(IEntity basedOnThisEntity)
  {
    if (basedOnThisEntity is not UserOrganization)
    {
      throw new InvalidOperationException();
    }
    else
    {
      UserOrganization userOrganization = (basedOnThisEntity as UserOrganization)!;

      this.UserId = userOrganization.UserId;
      this.OrganizationId = userOrganization.OrganizationId;
    }
  }
}
