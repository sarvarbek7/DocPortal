using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class UserOrganization : IEntity<int>
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public int OrganizationId { get; set; }
  public User Admin { get; set; }
  public Organization AssignedOrganization { get; set; }
}
