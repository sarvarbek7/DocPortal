using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
{
  public void Configure(EntityTypeBuilder<Organization> builder)
  {
    builder.ToTable("organizations");

    builder.HasIndex(org => org.Title);

    builder.HasIndex(org => org.PhysicalIdentity).IsUnique(true);

    builder.HasKey(entity => entity.Id);

    // IEntity
    builder.Property(entity => entity.Id)
      .HasColumnName("id");

    // IAuditableEntity

    builder.Property(entity => entity.CreatedBy)
      .HasColumnName("created_by");

    builder.Property(entity => entity.UpdatedBy)
      .HasColumnName("updated_by");

    builder.Property(entity => entity.CreatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("created_at");

    builder.Property(entity => entity.UpdatedAt)
      .HasColumnType("timestamp")
      .HasColumnName("updated_at");

    // ISoftDeletedEntity
    builder.Property(entity => entity.IsDeleted)
      .HasColumnName("is_deleted");

    // Custom
    builder.Property(org => org.Title)
      .HasMaxLength(1023)
      .HasColumnName("title");

    builder.Property(org => org.PrimaryOrganizationId)
      .HasColumnName("primary_organization_id");

    builder.Property(org => org.PhysicalIdentity)
      .HasMaxLength(255)
      .HasColumnName("physical_identity");

    builder.Property(org => org.Details)
      .HasMaxLength(255)
      .HasColumnName("details");

    builder.HasMany(organization => organization.Documents)
      .WithOne(document => document.Organization)
      .HasForeignKey(document => document.OrganizationId);

    builder.HasMany(current => current.Subordinates)
      .WithOne(navigated => navigated.PrimaryOrganization)
      .HasForeignKey(navigated => navigated.PrimaryOrganizationId);

    builder.HasOne(current => current.PrimaryOrganization)
      .WithMany(navigated => navigated.Subordinates)
      .HasForeignKey(current => current.PrimaryOrganizationId)
      .OnDelete(DeleteBehavior.SetNull);

    builder.HasMany(organization => organization.Admins)
      .WithOne(role => role.AssignedOrganization)
      .HasForeignKey(role => role.OrganizationId);
  }
}
