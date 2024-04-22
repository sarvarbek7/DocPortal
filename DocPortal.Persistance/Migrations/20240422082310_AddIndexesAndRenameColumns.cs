using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
  /// <inheritdoc />
  public partial class AddIndexesAndRenameColumns : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Document_DocumentType_DocumentTypeId",
          table: "Document");

      migrationBuilder.DropForeignKey(
          name: "FK_Document_Organization_OrganizationId",
          table: "Document");

      migrationBuilder.DropForeignKey(
          name: "FK_Organization_Organization_PrimaryOrganizationId",
          table: "Organization");

      migrationBuilder.DropForeignKey(
          name: "FK_UserRole_Organization_OrganizationId",
          table: "UserRole");

      migrationBuilder.DropForeignKey(
          name: "FK_UserRole_User_UserId",
          table: "UserRole");

      migrationBuilder.DropPrimaryKey(
          name: "PK_UserRole",
          table: "UserRole");

      migrationBuilder.DropPrimaryKey(
          name: "PK_User",
          table: "User");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Organization",
          table: "Organization");

      migrationBuilder.DropPrimaryKey(
          name: "PK_DocumentType",
          table: "DocumentType");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Document",
          table: "Document");

      migrationBuilder.DropColumn(
          name: "Role",
          table: "UserRole");

      migrationBuilder.RenameTable(
          name: "UserRole",
          newName: "roles");

      migrationBuilder.RenameTable(
          name: "User",
          newName: "users");

      migrationBuilder.RenameTable(
          name: "Organization",
          newName: "organizations");

      migrationBuilder.RenameTable(
          name: "DocumentType",
          newName: "documentTypes");

      migrationBuilder.RenameTable(
          name: "Document",
          newName: "documents");

      migrationBuilder.RenameIndex(
          name: "IX_UserRole_UserId",
          table: "roles",
          newName: "IX_roles_UserId");

      migrationBuilder.RenameIndex(
          name: "IX_UserRole_OrganizationId",
          table: "roles",
          newName: "IX_roles_OrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_Organization_PrimaryOrganizationId",
          table: "organizations",
          newName: "IX_organizations_PrimaryOrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_Document_OrganizationId",
          table: "documents",
          newName: "IX_documents_OrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_Document_DocumentTypeId",
          table: "documents",
          newName: "IX_documents_DocumentTypeId");

      migrationBuilder.AddColumn<string>(
          name: "Role",
          table: "users",
          type: "character varying(31)",
          maxLength: 31,
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddPrimaryKey(
          name: "PK_roles",
          table: "roles",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_users",
          table: "users",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_organizations",
          table: "organizations",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_documentTypes",
          table: "documentTypes",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_documents",
          table: "documents",
          column: "Id");

      migrationBuilder.CreateIndex(
          name: "IX_users_Login",
          table: "users",
          column: "Login",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_organizations_Title",
          table: "organizations",
          column: "Title");

      migrationBuilder.CreateIndex(
          name: "IX_documents_RegisteredNumber",
          table: "documents",
          column: "RegisteredNumber");

      migrationBuilder.CreateIndex(
          name: "IX_documents_Title",
          table: "documents",
          column: "Title");

      migrationBuilder.AddForeignKey(
          name: "FK_documents_documentTypes_DocumentTypeId",
          table: "documents",
          column: "DocumentTypeId",
          principalTable: "documentTypes",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);

      migrationBuilder.AddForeignKey(
          name: "FK_documents_organizations_OrganizationId",
          table: "documents",
          column: "OrganizationId",
          principalTable: "organizations",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);

      migrationBuilder.AddForeignKey(
          name: "FK_organizations_organizations_PrimaryOrganizationId",
          table: "organizations",
          column: "PrimaryOrganizationId",
          principalTable: "organizations",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_roles_organizations_OrganizationId",
          table: "roles",
          column: "OrganizationId",
          principalTable: "organizations",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_roles_users_UserId",
          table: "roles",
          column: "UserId",
          principalTable: "users",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_documents_documentTypes_DocumentTypeId",
          table: "documents");

      migrationBuilder.DropForeignKey(
          name: "FK_documents_organizations_OrganizationId",
          table: "documents");

      migrationBuilder.DropForeignKey(
          name: "FK_organizations_organizations_PrimaryOrganizationId",
          table: "organizations");

      migrationBuilder.DropForeignKey(
          name: "FK_roles_organizations_OrganizationId",
          table: "roles");

      migrationBuilder.DropForeignKey(
          name: "FK_roles_users_UserId",
          table: "roles");

      migrationBuilder.DropPrimaryKey(
          name: "PK_users",
          table: "users");

      migrationBuilder.DropIndex(
          name: "IX_users_Login",
          table: "users");

      migrationBuilder.DropPrimaryKey(
          name: "PK_roles",
          table: "roles");

      migrationBuilder.DropPrimaryKey(
          name: "PK_organizations",
          table: "organizations");

      migrationBuilder.DropIndex(
          name: "IX_organizations_Title",
          table: "organizations");

      migrationBuilder.DropPrimaryKey(
          name: "PK_documentTypes",
          table: "documentTypes");

      migrationBuilder.DropPrimaryKey(
          name: "PK_documents",
          table: "documents");

      migrationBuilder.DropIndex(
          name: "IX_documents_RegisteredNumber",
          table: "documents");

      migrationBuilder.DropIndex(
          name: "IX_documents_Title",
          table: "documents");

      migrationBuilder.DropColumn(
          name: "Role",
          table: "users");

      migrationBuilder.RenameTable(
          name: "users",
          newName: "User");

      migrationBuilder.RenameTable(
          name: "roles",
          newName: "UserRole");

      migrationBuilder.RenameTable(
          name: "organizations",
          newName: "Organization");

      migrationBuilder.RenameTable(
          name: "documentTypes",
          newName: "DocumentType");

      migrationBuilder.RenameTable(
          name: "documents",
          newName: "Document");

      migrationBuilder.RenameIndex(
          name: "IX_roles_UserId",
          table: "UserRole",
          newName: "IX_UserRole_UserId");

      migrationBuilder.RenameIndex(
          name: "IX_roles_OrganizationId",
          table: "UserRole",
          newName: "IX_UserRole_OrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_organizations_PrimaryOrganizationId",
          table: "Organization",
          newName: "IX_Organization_PrimaryOrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_documents_OrganizationId",
          table: "Document",
          newName: "IX_Document_OrganizationId");

      migrationBuilder.RenameIndex(
          name: "IX_documents_DocumentTypeId",
          table: "Document",
          newName: "IX_Document_DocumentTypeId");

      migrationBuilder.AddColumn<string>(
          name: "Role",
          table: "UserRole",
          type: "character varying(31)",
          maxLength: 31,
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddPrimaryKey(
          name: "PK_User",
          table: "User",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_UserRole",
          table: "UserRole",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_Organization",
          table: "Organization",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_DocumentType",
          table: "DocumentType",
          column: "Id");

      migrationBuilder.AddPrimaryKey(
          name: "PK_Document",
          table: "Document",
          column: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_Document_DocumentType_DocumentTypeId",
          table: "Document",
          column: "DocumentTypeId",
          principalTable: "DocumentType",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Document_Organization_OrganizationId",
          table: "Document",
          column: "OrganizationId",
          principalTable: "Organization",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_Organization_Organization_PrimaryOrganizationId",
          table: "Organization",
          column: "PrimaryOrganizationId",
          principalTable: "Organization",
          principalColumn: "Id");

      migrationBuilder.AddForeignKey(
          name: "FK_UserRole_Organization_OrganizationId",
          table: "UserRole",
          column: "OrganizationId",
          principalTable: "Organization",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "FK_UserRole_User_UserId",
          table: "UserRole",
          column: "UserId",
          principalTable: "User",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
