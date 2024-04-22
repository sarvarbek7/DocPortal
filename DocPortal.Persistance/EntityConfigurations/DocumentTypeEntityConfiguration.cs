﻿using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentType>
{
  public void Configure(EntityTypeBuilder<DocumentType> builder)
  {
    builder.ToTable("documentTypes");

    builder.HasKey(docType => docType.Id);

    builder.Property(docType => docType.Id)
      .HasColumnName("id");

    builder.Property(docType => docType.Title)
      .HasMaxLength(1023)
      .HasColumnName("title");

    builder.HasMany(docType => docType.Documents)
      .WithOne(document => document.DocumentType)
      .HasForeignKey(document => document.DocumentTypeId);
  }
}