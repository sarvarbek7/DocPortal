namespace DocPortal.Domain.Common.Entities;

public interface ISoftDeteledEntity<TAuditId> : ISoftDeletedEntity
  where TAuditId : struct
{
  TAuditId? DeletedBy { get; set; }
}

public interface ISoftDeletedEntity
{
  bool IsDeleted { get; set; }
  DateTime? DeletedAt { get; set; }

  public void Undo()
  {
    IsDeleted = false;
    DeletedAt = null;
  }
}