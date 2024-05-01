using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class Document : Entity<Guid, int>
{
  public string Title { get; set; }
  public string RegisteredNumber { get; set; }
  public DateOnly RegisteredDate { get; set; }
  public bool IsPrivate { get; set; } = false;
  public string StoragePath { get; set; }
  public int OrganizationId { get; set; }
  public int DocumentTypeId { get; set; }
  public Organization? Organization { get; set; }
  public DocumentType? DocumentType { get; set; }

  public override void UpdateEntityState(IEntity basedOnThisEntity)
  {
    if (basedOnThisEntity is not Document)
    {
      throw new InvalidOperationException();
    }
    else
    {
      Document document = (basedOnThisEntity as Document)!;

      this.Title = document.Title;
      this.DocumentTypeId = document.DocumentTypeId;
      this.OrganizationId = document.OrganizationId;
      this.RegisteredNumber = document.RegisteredNumber;
      this.RegisteredDate = document.RegisteredDate;
      this.IsPrivate = document.IsPrivate;
    }
  }
}
