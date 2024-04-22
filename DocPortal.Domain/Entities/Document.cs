using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class Document : IEntity<Guid>, IAuditableEntity<int>
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string RegisteredNumber { get; set; }
  public DateOnly RegisteredDate { get; set; }
  public bool IsPrivate { get; set; } = false;
  public string StoragePath { get; set; }
  public int OrganizationId { get; set; }
  public int DocumentTypeId { get; set; }
  public Organization? Organization { get; set; }
  public DocumentType? DocumentType { get; set; }
  public int CreatedBy { get; set; }
  public int UpdatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
