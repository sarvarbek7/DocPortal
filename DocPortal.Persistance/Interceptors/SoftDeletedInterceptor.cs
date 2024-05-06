using DocPortal.Domain.Common.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocPortal.Persistance.Interceptors;

internal sealed class SoftDeletedInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    DbContext? context = eventData.Context;
    if (context is not null)
    {
      var entries =
        context.ChangeTracker.Entries<ISoftDeletedEntity>().ToList();

      foreach (var entiry in from EntityEntry<ISoftDeletedEntity> entiry in entries
                             where entiry.State is EntityState.Deleted
                             select entiry)
      {
        entiry.Property(nameof(ISoftDeletedEntity.IsDeleted)).CurrentValue = true;
        entiry.Property(nameof(ISoftDeletedEntity.DeletedAt)).CurrentValue = DateTime.Now;

        if (entiry.Entity is ISoftDeteledEntity<int>)
        {
          entiry.Property(nameof(ISoftDeteledEntity<int>.DeletedBy)).CurrentValue =
            entiry.CurrentValues.GetValue<int?>(nameof(ISoftDeteledEntity<int>.DeletedBy));
        }

        entiry.State = EntityState.Modified;
      }
    }

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }
}
