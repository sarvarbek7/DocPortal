using DocPortal.Domain.Audit;
using DocPortal.Domain.Common.Entities;

namespace DocPortal.Application.Services.Processing
{
  public interface IAuditService<TEntityId>
    where TEntityId : struct
  {
    public ValueTask<(AuditInfo.Created, AuditInfo.Updated)> BasicAuditInfo(TEntityId entityId);
    public ValueTask<AuditInfo.Deleted> DeletedAuditInfo<TSoftDeletedEntity>(TEntityId entityId)
      where TSoftDeletedEntity : class, IEntity<TEntityId>, ISoftDeteledEntity<int>;
  }
}
