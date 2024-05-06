using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCsToUserPhysicalIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_physical_identity",
                table: "users",
                column: "physical_identity",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_physical_identity",
                table: "users");
        }
    }
}
