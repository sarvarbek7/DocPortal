using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations
{
  internal class UserCredentialEntityConfiguration : IEntityTypeConfiguration<UserCredential>
  {
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
      builder.ToTable("user_credentials");

      builder.HasKey(entity => entity.Id);

      builder.HasIndex(credential => credential.Login).IsUnique();

      //////////////////////////////////////////////////////////////////////

      // IEntity
      builder.Property(entity => entity.Id)
        .HasColumnName("id");

      //////////////////////////////////////////////////////////////////////

      builder.Property(credential => credential.Login)
      .HasMaxLength(127)
      .HasColumnName("login");

      builder.Property(credential => credential.Password)
      .HasMaxLength(63)
      .HasColumnName("password");
    }
  }
}
