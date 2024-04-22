using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasKey(entity => entity.Id);

    builder.HasIndex(user => user.Login).IsUnique();

    // IEntity
    builder.Property(entity => entity.Id)
      .HasColumnName("id");

    // IAuditableEntity

    builder.Property(entity => entity.CreatedBy)
      .HasColumnName("createdBy");

    builder.Property(entity => entity.UpdatedBy)
      .HasColumnName("updatedBy");

    builder.Property(entity => entity.CreatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("createdAt");

    builder.Property(entity => entity.UpdatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("updatedAt");

    // ISoftDeletedEntity
    builder.Property(entity => entity.IsDeleted)
      .HasColumnName("isDeleted");

    // Custom

    builder.Property(user => user.PhysicalIdentity)
      .HasColumnType("char(14)")
      .HasColumnName("physicalIdentity");

    builder.Property(user => user.FirstName)
      .HasMaxLength(127)
      .HasColumnName("firstName");

    builder.Property(user => user.LastName)
      .HasMaxLength(127)
      .HasColumnName("lastName");

    builder.Property(user => user.JobPosition)
      .HasMaxLength(127)
      .HasColumnName("jobPosition");

    builder.Property(user => user.Login)
      .HasMaxLength(127)
      .HasColumnName("login");

    builder.Property(role => role.Role)
      .HasMaxLength(31)
      .HasColumnName("role");

    builder.Property(user => user.PasswordHash)
      .HasMaxLength(63)
      .HasColumnName("passwordHash");

    builder.HasMany(user => user.UserOrganizations)
      .WithOne(userOrg => userOrg.Admin)
      .HasForeignKey(userOrg => userOrg.UserId);

    builder.Navigation(user => user.UserOrganizations).AutoInclude();
  }
}
