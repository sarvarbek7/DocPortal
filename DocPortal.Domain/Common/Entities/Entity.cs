
namespace DocPortal.Domain.Common.Entities
{
  public abstract class Entity<TId, TAuditId> : IEntity<TId>, IAuditableEntity<TAuditId>, ISoftDeletedEntity
    where TId : struct
    where TAuditId : struct
  {
    public TId Id { get; set; }
    public TAuditId CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public TAuditId UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
  }
}
