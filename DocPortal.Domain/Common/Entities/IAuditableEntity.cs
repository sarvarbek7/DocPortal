namespace DocPortal.Domain.Common.Entities;

public interface IAuditableEntity<TAuditId> : IAuditableEntity
  where TAuditId : struct
{
  TAuditId CreatedBy { get; set; }
  TAuditId? UpdatedBy { get; set; }
}

public interface IAuditableEntity
{
  DateTime CreatedAt { get; set; }
  DateTime? UpdatedAt { get; set; }
}
