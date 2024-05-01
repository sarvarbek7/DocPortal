
namespace DocPortal.Domain.Common.Entities
{
  public abstract class Entity<TId, TAuditId> : IEntity<TId>, IAuditableEntity<TAuditId>, ISoftDeteledEntity<TAuditId>
    where TId : struct
    where TAuditId : struct
  {
    public TId Id { get; set; }

    // Auditable feature
    public TAuditId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public TAuditId? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Soft deleted feature
    public bool IsDeleted { get; set; }
    public TAuditId? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }

    public abstract void UpdateEntityState(IEntity basedOnThisEntity);
  }
}
