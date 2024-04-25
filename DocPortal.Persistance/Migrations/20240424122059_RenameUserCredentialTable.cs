using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
  /// <inheritdoc />
  public partial class RenameUserCredentialTable : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_user_credential_users_id",
          table: "user_credential");

      migrationBuilder.DropPrimaryKey(
          name: "PK_user_credential",
          table: "user_credential");

      migrationBuilder.RenameTable(
          name: "user_credential",
          newName: "user_credentials");

      migrationBuilder.RenameIndex(
          name: "IX_user_credential_login",
          table: "user_credentials",
          newName: "IX_user_credentials_login");

      migrationBuilder.AddPrimaryKey(
          name: "PK_user_credentials",
          table: "user_credentials",
          column: "id");

      migrationBuilder.AddForeignKey(
          name: "FK_user_credentials_users_id",
          table: "user_credentials",
          column: "id",
          principalTable: "users",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_user_credentials_users_id",
          table: "user_credentials");

      migrationBuilder.DropPrimaryKey(
          name: "PK_user_credentials",
          table: "user_credentials");

      migrationBuilder.RenameTable(
          name: "user_credentials",
          newName: "user_credential");

      migrationBuilder.RenameIndex(
          name: "IX_user_credentials_login",
          table: "user_credential",
          newName: "IX_user_credential_login");

      migrationBuilder.AddPrimaryKey(
          name: "PK_user_credential",
          table: "user_credential",
          column: "id");

      migrationBuilder.AddForeignKey(
          name: "FK_user_credential_users_id",
          table: "user_credential",
          column: "id",
          principalTable: "users",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
