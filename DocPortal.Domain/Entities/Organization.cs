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
  public ICollection<UserOrganization>? AssignedRoles { get; set; }
  public ICollection<Document>? Documents { get; set; }
}