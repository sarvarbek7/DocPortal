using DocPortal.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocPortal.Persistance.EntityConfigurations;

internal sealed class UserOrganizationEntityConfiguration : IEntityTypeConfiguration<UserOrganization>
{
  public void Configure(EntityTypeBuilder<UserOrganization> builder)
  {
    builder.ToTable("userOrganizations");

    builder.HasKey(entity => entity.Id);

    builder.HasIndex(userOrganization => userOrganization.UserId).IsUnique(false);

    builder.Property(userOrganization => userOrganization.Id)
      .HasColumnName("id");

    builder.Property(userOrganization => userOrganization.UserId)
      .HasColumnName("userId");

    builder.Property(userOrganization => userOrganization.OrganizationId)
      .HasColumnName("organizationId");

    builder.HasOne(role => role.AssignedOrganization)
      .WithMany(org => org.AssignedRoles)
      .HasForeignKey(role => role.OrganizationId);

    builder.HasOne(role => role.Admin)
      .WithMany(user => user.UserOrganizations)
      .HasForeignKey(role => role.UserId);
  }
}
