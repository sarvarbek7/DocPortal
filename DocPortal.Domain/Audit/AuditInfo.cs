namespace DocPortal.Domain.Audit;

public record AuditInfo
{
  public record Created(int? CreatedBy,
                        string? CreatedByFullName,
                        DateTime CreateAt);

  public record Updated(int? UpdatedBy,
                        string? UpdatedByFullName,
                        DateTime? UpdatedAt);

  public record Deleted(int? DeletedBy,
                        string? DeletedByFullName,
                        DateTime? DeletedAt);
}

