using DocPortal.Domain.Common.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocPortal.Persistance.Interceptors;

internal sealed class PrimaryKeyInterceptor : SaveChangesInterceptor
{
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData eventData,
      InterceptionResult<int> result,
      CancellationToken cancellationToken = new CancellationToken())
  {
    var entries = eventData.Context!.ChangeTracker.Entries<IEntity<Guid>>().ToList();

    // Set Primary keys of newly added entities.
    entries.ForEach(entry =>
    {
      if (entry.State == EntityState.Added && entry.Properties.Any(property => property.Metadata.Name.Equals(nameof(IEntity<Guid>.Id))))
        entry.Property(nameof(IEntity<Guid>.Id)).CurrentValue = Guid.NewGuid();
    });

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }
}
