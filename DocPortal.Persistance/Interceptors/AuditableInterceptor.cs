using DocPortal.Domain.Common.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocPortal.Persistance.Interceptors;

internal sealed class AuditableInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    if (eventData.Context is not null)
    {
      UpdateAuditableEntities(eventData.Context);
    }

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static void UpdateAuditableEntities(DbContext context)
  {
    DateTime utcNow = DateTime.Now;
    var entries = context.ChangeTracker.Entries<IAuditableEntity>().ToList();

    foreach (EntityEntry<IAuditableEntity> entry in entries)
    {
      if (entry.State == EntityState.Added)
      {
        SetCurrentPropertyValue(
            entry, nameof(IAuditableEntity.CreatedAt), utcNow);
        SetCurrentPropertyValue(
            entry, nameof(IAuditableEntity.UpdatedAt), utcNow);
      }

      if (entry.State == EntityState.Modified)
      {
        SetCurrentPropertyValue(
            entry, nameof(IAuditableEntity.UpdatedAt), utcNow);
      }
    }

    static void SetCurrentPropertyValue(
        EntityEntry entry,
        string propertyName,
        DateTime utcNow) =>
        entry.Property(propertyName).CurrentValue = utcNow;
  }

}
