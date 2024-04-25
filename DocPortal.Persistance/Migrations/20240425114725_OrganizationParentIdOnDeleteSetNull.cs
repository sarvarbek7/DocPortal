using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class OrganizationParentIdOnDeleteSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations");

            migrationBuilder.AddForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations",
                column: "primary_organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations");

            migrationBuilder.AddForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations",
                column: "primary_organization_id",
                principalTable: "organizations",
                principalColumn: "id");
        }
    }
}
