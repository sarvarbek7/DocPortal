using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UserCredentialsAreMoveItsSeparateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_login",
                table: "users");

            migrationBuilder.DropColumn(
                name: "login",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password",
                table: "users");

            migrationBuilder.CreateTable(
                name: "user_credential",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    login = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
                    password = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_credential", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_credential_users_id",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_credential_login",
                table: "user_credential",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_credential");

            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "users",
                type: "character varying(127)",
                maxLength: 127,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                type: "character varying(63)",
                maxLength: 63,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_login",
                table: "users",
                column: "login",
                unique: true);
        }
    }
}
