using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationPhysicalIdentityIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_organizations_physical_identity",
                table: "organizations",
                column: "physical_identity",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_organizations_physical_identity",
                table: "organizations");
        }
    }
}
