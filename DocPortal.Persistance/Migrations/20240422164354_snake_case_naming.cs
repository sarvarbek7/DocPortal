using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class snake_case_naming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_documentTypes_documentTypeId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_userOrganizations",
                table: "userOrganizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_documentTypes",
                table: "documentTypes");

            migrationBuilder.RenameTable(
                name: "userOrganizations",
                newName: "user_organizations");

            migrationBuilder.RenameTable(
                name: "documentTypes",
                newName: "document_types");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "users",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "users",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "physicalIdentity",
                table: "users",
                newName: "physical_identity");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "jobPosition",
                table: "users",
                newName: "job_position");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "users",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "users",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "organizations",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "organizations",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "primaryOrganizationId",
                table: "organizations",
                newName: "primary_organization_id");

            migrationBuilder.RenameColumn(
                name: "physicalIdentity",
                table: "organizations",
                newName: "physical_identity");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "organizations",
                newName: "is_deleted");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "organizations",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "organizations",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_primaryOrganizationId",
                table: "organizations",
                newName: "IX_organizations_primary_organization_id");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "user_organizations",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "user_organizations",
                newName: "organization_id");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_userId",
                table: "user_organizations",
                newName: "IX_user_organizations_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_userOrganizations_organizationId",
                table: "user_organizations",
                newName: "IX_user_organizations_organization_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_organizations",
                table: "user_organizations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_document_types",
                table: "document_types",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_document_types_documentTypeId",
                table: "documents",
                column: "documentTypeId",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations",
                column: "primary_organization_id",
                principalTable: "organizations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_organizations_organizations_organization_id",
                table: "user_organizations",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_organizations_users_user_id",
                table: "user_organizations",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_document_types_documentTypeId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_organizations_organizations_primary_organization_id",
                table: "organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_user_organizations_organizations_organization_id",
                table: "user_organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_user_organizations_users_user_id",
                table: "user_organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_organizations",
                table: "user_organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_document_types",
                table: "document_types");

            migrationBuilder.RenameTable(
                name: "user_organizations",
                newName: "userOrganizations");

            migrationBuilder.RenameTable(
                name: "document_types",
                newName: "documentTypes");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "users",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "users",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "users",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "physical_identity",
                table: "users",
                newName: "physicalIdentity");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "users",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "job_position",
                table: "users",
                newName: "jobPosition");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "users",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "users",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "organizations",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "organizations",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "primary_organization_id",
                table: "organizations",
                newName: "primaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "physical_identity",
                table: "organizations",
                newName: "physicalIdentity");

            migrationBuilder.RenameColumn(
                name: "is_deleted",
                table: "organizations",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "organizations",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "organizations",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_organizations_primary_organization_id",
                table: "organizations",
                newName: "IX_organizations_primaryOrganizationId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "userOrganizations",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "userOrganizations",
                newName: "organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_user_organizations_user_id",
                table: "userOrganizations",
                newName: "IX_userOrganizations_userId");

            migrationBuilder.RenameIndex(
                name: "IX_user_organizations_organization_id",
                table: "userOrganizations",
                newName: "IX_userOrganizations_organizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userOrganizations",
                table: "userOrganizations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documentTypes",
                table: "documentTypes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_documentTypes_documentTypeId",
                table: "documents",
                column: "documentTypeId",
                principalTable: "documentTypes",
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
    }
}
