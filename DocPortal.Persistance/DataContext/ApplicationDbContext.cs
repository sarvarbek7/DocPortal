using DocPortal.Domain.Entities;
using DocPortal.Persistance.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DocPortal.Persistance.DataContext;

public sealed class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions options)
    : base(options)
  {
  }

  public DbSet<Document> Documents { get; set; }
  public DbSet<DocumentType> DocumentTypes { get; set; }
  public DbSet<Organization> Organizations { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<UserOrganization> UserRoles { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    IInterceptor[] interceptors =
    [
      new AuditableInterceptor(),
      new SoftDeletedInterceptor(),
      new PrimaryKeyInterceptor()
    ];

    optionsBuilder.AddInterceptors(interceptors);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(
      assembly: typeof(ApplicationDbContext).Assembly);
  }
}
