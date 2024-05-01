using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class UserOrganizationEntityConfiguration : IEntityTypeConfiguration<UserOrganization>
{
  public void Configure(EntityTypeBuilder<UserOrganization> builder)
  {
    builder.ToTable("user_organizations");

    builder.HasKey(entity => entity.Id);

    builder.HasIndex(userOrganization => userOrganization.UserId).IsUnique(false);

    //////////////////////////////////////////////////////////////////////

    // IEntity
    builder.Property(entity => entity.Id)
      .HasColumnName("id");

    //////////////////////////////////////////////////////////////////////

    builder.Property(userOrganization => userOrganization.UserId)
      .HasColumnName("user_id");

    builder.Property(userOrganization => userOrganization.OrganizationId)
      .HasColumnName("organization_id");

    builder.HasOne(role => role.AssignedOrganization)
      .WithMany(org => org.Admins)
      .HasForeignKey(role => role.OrganizationId);

    builder.HasOne(role => role.Admin)
      .WithMany(user => user.UserOrganizations)
      .HasForeignKey(role => role.UserId);
  }
}
