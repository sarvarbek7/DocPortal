using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class Organization : Entity<int, int>
{
  public string Title { get; set; }
  public int? PrimaryOrganizationId { get; set; }
  public string PhysicalIdentity { get; set; }
  public string? Details { get; set; }
  public Organization? PrimaryOrganization { get; set; }
  public ICollection<Organization>? Subordinates { get; set; }
  public ICollection<UserOrganization>? Admins { get; set; }
  public ICollection<Document>? Documents { get; set; }

  public override void UpdateEntityState(IEntity basedOnThisEntity)
  {
    if (basedOnThisEntity is not Organization)
    {
      throw new InvalidOperationException();
    }
    else
    {
      Organization organization = (basedOnThisEntity as Organization)!;

      this.Title = organization.Title;
      this.PrimaryOrganizationId = organization.PrimaryOrganizationId;
      this.PhysicalIdentity = organization.PhysicalIdentity;
      this.Details = organization.Details;
    }
  }
}