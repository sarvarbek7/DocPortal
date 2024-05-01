using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
    builder.ToTable("documents");

    builder.HasQueryFilter(document => !document.IsDeleted);

    builder.HasKey(entity => entity.Id);

    builder.HasIndex(entity => entity.RegisteredNumber);

    builder.HasIndex(entity => entity.Title);

    builder.HasIndex(entity => entity.RegisteredDate);

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

    builder.Property(entity => entity.Title)
      .HasMaxLength(1023)
      .HasColumnName("title");

    builder.Property(entity => entity.RegisteredNumber)
      .HasMaxLength(63)
      .HasColumnName("registered_number");

    builder.Property(entity => entity.RegisteredDate)
      .HasColumnName("registered_date");

    builder.Property(entity => entity.IsPrivate)
      .HasColumnName("is_private");

    builder.Property(entity => entity.StoragePath)
      .HasColumnName("storage_path");

    builder.Property(entity => entity.OrganizationId)
      .HasColumnName("organization_id");

    builder.Property(entity => entity.DocumentTypeId)
      .HasColumnName("document_type_id");

    builder.HasOne(entity => entity.Organization)
      .WithMany(org => org.Documents)
      .HasForeignKey(entity => entity.OrganizationId);

    builder.HasOne(entity => entity.DocumentType)
      .WithMany(docType => docType.Documents)
      .HasForeignKey(entity => entity.DocumentTypeId);

    builder.Navigation(
      doctype => doctype.DocumentType).AutoInclude();
  }
}
