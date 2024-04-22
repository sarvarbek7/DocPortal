using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class customNamesForAllColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_userOrganizations_organizations_OrganizationId",
                table: "userOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_userOrganizations_users_UserId",
                table: "userOrganizations");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "users",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "users",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "PhysicalIdentity",
                table: "users",
                newName: "physicalIdentity");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "users",
                newName: "login");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "JobPosition",
                table: "users",
                newName: "jobPosition");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "users",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "users",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_users_Login",
                table: "users",
                newName: "IX_users_login");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "userOrganizations",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "userOrganizations",
                newName: "organizationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "userOrganizations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_UserId",
                table: "userOrganizations",
                newName: "IX_userOrganizations_userId");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_OrganizationId",
                table: "userOrganizations",
                newName: "IX_userOrganizations_organizationId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "organizations",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "organizations",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "organizations",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "PrimaryOrganizationId",
                table: "organizations",
                newName: "primaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "PhysicalIdentity",
                table: "organizations",
                newName: "physicalIdentity");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "organizations",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "organizations",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "organizations",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "organizations",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "organizations",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_Title",
                table: "organizations",
                newName: "IX_organizations_title");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_PrimaryOrganizationId",
                table: "organizations",
                newName: "IX_organizations_primaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "documentTypes",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "documentTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "documents",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "documents",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "documents",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "documents",
                newName: "storagePath");

            migrationBuilder.RenameColumn(
                name: "RegisteredNumber",
                table: "documents",
                newName: "registeredNumber");

            migrationBuilder.RenameColumn(
                name: "RegisteredDate",
                table: "documents",
                newName: "registeredDate");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "documents",
                newName: "organizationId");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "documents",
                newName: "isPrivate");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "documents",
                newName: "documentTypeId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "documents",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "documents",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "documents",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_documents_Title",
                table: "documents",
                newName: "IX_documents_title");

            migrationBuilder.RenameIndex(
                name: "IX_documents_RegisteredNumber",
                table: "documents",
                newName: "IX_documents_registeredNumber");

            migrationBuilder.RenameIndex(
                name: "IX_documents_OrganizationId",
                table: "documents",
                newName: "IX_documents_organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_documents_DocumentTypeId",
                table: "documents",
                newName: "IX_documents_documentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "details",
                table: "organizations",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_documentTypes_documentTypeId",
                table: "documents",
                column: "documentTypeId",
                principalTable: "documentTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_organizations_organizationId",
                table: "documents",
                column: "organizationId",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organizations_organizations_primaryOrganizationId",
                table: "organizations",
                column: "primaryOrganizationId",
                principalTable: "organizations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_userOrganizations_organizations_organizationId",
                table: "userOrganizations",
                column: "organizationId",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userOrganizations_users_userId",
                table: "userOrganizations",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_documentTypes_documentTypeId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_organizations_organizationId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_organizations_organizations_primaryOrganizationId",
                table: "organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_userOrganizations_organizations_organizationId",
                table: "userOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_userOrganizations_users_userId",
                table: "userOrganizations");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "users",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "physicalIdentity",
                table: "users",
                newName: "PhysicalIdentity");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "login",
                table: "users",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "jobPosition",
                table: "users",
                newName: "JobPosition");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "users",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "users",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "users",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_users_login",
                table: "users",
                newName: "IX_users_Login");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "userOrganizations",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "userOrganizations",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "userOrganizations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_userId",
                table: "userOrganizations",
                newName: "IX_userOrganizations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_organizationId",
                table: "userOrganizations",
                newName: "IX_userOrganizations_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "organizations",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "organizations",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "organizations",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "primaryOrganizationId",
                table: "organizations",
                newName: "PrimaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "physicalIdentity",
                table: "organizations",
                newName: "PhysicalIdentity");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "organizations",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "organizations",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "organizations",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "organizations",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "organizations",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_title",
                table: "organizations",
                newName: "IX_organizations_Title");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_primaryOrganizationId",
                table: "organizations",
                newName: "IX_organizations_PrimaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "documentTypes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "documentTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "documents",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "documents",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "documents",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "storagePath",
                table: "documents",
                newName: "StoragePath");

            migrationBuilder.RenameColumn(
                name: "registeredNumber",
                table: "documents",
                newName: "RegisteredNumber");

            migrationBuilder.RenameColumn(
                name: "registeredDate",
                table: "documents",
                newName: "RegisteredDate");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "documents",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "documents",
                newName: "IsPrivate");

            migrationBuilder.RenameColumn(
                name: "documentTypeId",
                table: "documents",
                newName: "DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "documents",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "documents",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "documents",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_documents_title",
                table: "documents",
                newName: "IX_documents_Title");

            migrationBuilder.RenameIndex(
                name: "IX_documents_registeredNumber",
                table: "documents",
                newName: "IX_documents_RegisteredNumber");

            migrationBuilder.RenameIndex(
                name: "IX_documents_organizationId",
                table: "documents",
                newName: "IX_documents_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_documents_documentTypeId",
                table: "documents",
                newName: "IX_documents_DocumentTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "organizations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_documentTypes_DocumentTypeId",
                table: "documents",
                column: "DocumentTypeId",
                principalTable: "documentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_organizations_OrganizationId",
                table: "documents",
                column: "OrganizationId",
                principalTable: "organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organizations_organizations_PrimaryOrganizationId",
                table: "organizations",
                column: "PrimaryOrganizationId",
                principalTable: "organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userOrganizations_organizations_OrganizationId",
                table: "userOrganizations",
                column: "OrganizationId",
                principalTable: "organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userOrganizations_users_UserId",
                table: "userOrganizations",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
