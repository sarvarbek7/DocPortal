using DocPortal.Domain.Common.Entities;

namespace DocPortal.Domain.Entities;

public sealed class DocumentType : IEntity<int>
{
  public int Id { get; set; }
  public string Title { get; set; }
  public ICollection<Document>? Documents { get; set; }
}
