using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasQueryFilter(user => !user.IsDeleted);

    builder.HasKey(entity => entity.Id);

    //////////////////////////////////////////////////////////////////////

    // IEntity
    builder.Property(entity => entity.Id)
      .HasColumnName("id");

    // IAuditableEntity
    builder.Property(auditableEntity => auditableEntity.CreatedBy)
      .HasColumnName("created_by");

    builder.Property(auditableEntity => auditableEntity.UpdatedBy)
      .HasColumnName("updated_by");

    builder.Property(auditableEntity => auditableEntity.CreatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("created_at");

    builder.Property(auditableEntity => auditableEntity.UpdatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("updated_at");

    // ISoftDeletedEntity
    builder.Property(softDeletedEntity => softDeletedEntity.IsDeleted)
      .HasColumnName("is_deleted");

    builder.Property(softDeletedEntity => softDeletedEntity.DeletedBy)
      .HasColumnName("deleted_by");

    builder.Property(softDeletedEntity => softDeletedEntity.DeletedAt)
      .HasColumnType("timestamp")
      .HasColumnName("deleted_at");

    //////////////////////////////////////////////////////////////////////

    // Custom

    builder.Property(user => user.PhysicalIdentity)
      .HasColumnType("char(14)")
      .HasColumnName("physical_identity");

    builder.Property(user => user.FirstName)
      .HasMaxLength(127)
      .HasColumnName("firstname");

    builder.Property(user => user.LastName)
      .HasMaxLength(127)
      .HasColumnName("lastname");

    builder.Property(user => user.JobPosition)
      .HasMaxLength(127)
      .HasColumnName("job_position");



    builder.Property(role => role.Role)
      .HasMaxLength(31)
      .HasColumnName("role");


    builder.HasMany(user => user.UserOrganizations)
      .WithOne(userOrg => userOrg.Admin)
      .HasForeignKey(userOrg => userOrg.UserId);

    builder.HasOne(user => user.UserCredential)
      .WithOne()
      .HasForeignKey<UserCredential>(cred => cred.Id);
  }
}
