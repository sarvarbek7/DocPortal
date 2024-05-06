using System.Linq.Expressions;

using DocPortal.Application.Services.Processing;
using DocPortal.Domain.Audit;
using DocPortal.Domain.Common.Entities;
using DocPortal.Domain.Entities;
using DocPortal.Persistance.DataContext;

using Microsoft.EntityFrameworkCore;

namespace DocPortal.Infrastructure.Services.Processing
{
  public class AuditService<TEntity, TEntityId>(ApplicationDbContext context) : IAuditService<TEntityId>
    where TEntity : class, IEntity<TEntityId>, IAuditableEntity<int>
    where TEntityId : struct
  {
    public async ValueTask<(AuditInfo.Created, AuditInfo.Updated)> BasicAuditInfo(TEntityId entityId)
    {
      var info = await context.Set<TEntity>().AsQueryable()
        .IgnoreQueryFilters().Select(ent => new
        {
          Id = ent.Id,
          CreatedBy = ent.CreatedBy,
          CreatedAt = ent.CreatedAt,
          UpdatedBy = ent.UpdatedBy,
          UpdatedAt = ent.UpdatedAt
        }).FirstOrDefaultAsync(sel => sel.Id.Equals(entityId));

      if (info is null)
      {
        throw new InvalidOperationException();
      }

      List<int> audits = [info.CreatedBy];

      if (info.UpdatedBy is { } updatedBy)
      {
        audits.Add(updatedBy);
      }

      Expression<Func<User, bool>> predicate = user => audits.Contains(user.Id);

      var usersWhichAreAudited = context.Set<User>().AsQueryable().IgnoreQueryFilters()
        .Where(predicate)
        .Select(user => new
        {
          Id = user.Id,
          FullName = user.LastName + " " + user.FirstName
        }).ToList();

      AuditInfo.Created createdAudit = new(info.CreatedBy,
                                           usersWhichAreAudited.Find(a => a.Id == info.CreatedBy)?.FullName,
                                           info.CreatedAt);

      AuditInfo.Updated updatedAudit = new(info.UpdatedBy,
                                           usersWhichAreAudited.Find(a => a.Id == info.UpdatedBy)?.FullName,
                                           info.UpdatedAt);

      return (createdAudit, updatedAudit);
    }

    public async ValueTask<AuditInfo.Deleted> DeletedAuditInfo<TSoftDeletedEntity>(TEntityId entityId)
      where TSoftDeletedEntity : class, IEntity<TEntityId>, ISoftDeteledEntity<int>
    {
      var info = await context.Set<TSoftDeletedEntity>().AsQueryable().IgnoreQueryFilters()
        .Select(ent => new
        {
          Id = ent.Id,
          DeletedBy = ent.DeletedBy,
          DeletedAt = ent.DeletedAt
        }).FirstOrDefaultAsync(a => a.Id.Equals(entityId));

      if (info is null)
      {
        throw new InvalidOperationException();
      }

      var userWhichAreAuditedForDeletion = await context.Set<User>().AsQueryable().IgnoreQueryFilters()
        .Select(user => new
        {
          Id = user.Id,
          FullName = user.LastName + " " + user.FirstName
        }).FirstOrDefaultAsync(user => user.Id == info.DeletedBy);

      return new AuditInfo.Deleted(info?.DeletedBy,
                                   userWhichAreAuditedForDeletion?.FullName,
                                   info?.DeletedAt);
    }
  }
}
