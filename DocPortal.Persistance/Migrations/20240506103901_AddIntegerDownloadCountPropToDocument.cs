using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddIntegerDownloadCountPropToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "download_count",
                table: "documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "download_count",
                table: "documents");
        }
    }
}
