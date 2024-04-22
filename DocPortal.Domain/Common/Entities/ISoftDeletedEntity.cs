namespace DocPortal.Domain.Common.Entities;

public interface ISoftDeletedEntity
{
  bool IsDeleted { get; set; }

  public void Undo()
  {
    IsDeleted = false;
  }
}