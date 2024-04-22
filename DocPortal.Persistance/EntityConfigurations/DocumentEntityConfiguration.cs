using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
  public void Configure(EntityTypeBuilder<Document> builder)
  {
    builder.ToTable("documents");

    builder.HasKey(entity => entity.Id);

    builder.HasIndex(entity => entity.RegisteredNumber);

    builder.HasIndex(entity => entity.Title).IsUnique(false);

    builder.Property(entity => entity.Id)
      .HasColumnName("id");

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

    builder.Property(entity => entity.Title)
      .HasMaxLength(1023)
      .HasColumnName("title");

    builder.Property(entity => entity.RegisteredNumber)
      .HasMaxLength(63)
      .HasColumnName("registeredNumber");

    builder.Property(entity => entity.RegisteredDate)
      .HasColumnName("registeredDate");

    builder.Property(entity => entity.IsPrivate)
      .HasColumnName("isPrivate");

    builder.Property(entity => entity.StoragePath)
      .HasColumnName("storagePath");

    builder.Property(entity => entity.OrganizationId)
      .HasColumnName("organizationId");

    builder.Property(entity => entity.DocumentTypeId)
      .HasColumnName("documentTypeId");

    builder.HasOne(entity => entity.Organization)
      .WithMany(org => org.Documents)
      .HasForeignKey(entity => entity.OrganizationId);

    builder.HasOne(entity => entity.DocumentType)
      .WithMany(docType => docType.Documents)
      .HasForeignKey(entity => entity.DocumentTypeId);
  }
}
