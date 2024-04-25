using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class snake_case_namingDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_document_types_documentTypeId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_organizations_organizationId",
                table: "documents");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "documents",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "documents",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "storagePath",
                table: "documents",
                newName: "storage_path");

            migrationBuilder.RenameColumn(
                name: "registeredNumber",
                table: "documents",
                newName: "registered_number");

            migrationBuilder.RenameColumn(
                name: "registeredDate",
                table: "documents",
                newName: "registered_date");

            migrationBuilder.RenameColumn(
                name: "organizationId",
                table: "documents",
                newName: "organization_id");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "documents",
                newName: "is_private");

            migrationBuilder.RenameColumn(
                name: "documentTypeId",
                table: "documents",
                newName: "document_type_id");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "documents",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "documents",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_documents_registeredNumber",
                table: "documents",
                newName: "IX_documents_registered_number");

            migrationBuilder.RenameIndex(
                name: "IX_documents_organizationId",
                table: "documents",
                newName: "IX_documents_organization_id");

            migrationBuilder.RenameIndex(
                name: "IX_documents_documentTypeId",
                table: "documents",
                newName: "IX_documents_document_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_document_types_document_type_id",
                table: "documents",
                column: "document_type_id",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_organizations_organization_id",
                table: "documents",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_document_types_document_type_id",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_organizations_organization_id",
                table: "documents");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "documents",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "documents",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "storage_path",
                table: "documents",
                newName: "storagePath");

            migrationBuilder.RenameColumn(
                name: "registered_number",
                table: "documents",
                newName: "registeredNumber");

            migrationBuilder.RenameColumn(
                name: "registered_date",
                table: "documents",
                newName: "registeredDate");

            migrationBuilder.RenameColumn(
                name: "organization_id",
                table: "documents",
                newName: "organizationId");

            migrationBuilder.RenameColumn(
                name: "is_private",
                table: "documents",
                newName: "isPrivate");

            migrationBuilder.RenameColumn(
                name: "document_type_id",
                table: "documents",
                newName: "documentTypeId");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "documents",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "documents",
                newName: "createdAt");

            migrationBuilder.RenameIndex(
                name: "IX_documents_registered_number",
                table: "documents",
                newName: "IX_documents_registeredNumber");

            migrationBuilder.RenameIndex(
                name: "IX_documents_organization_id",
                table: "documents",
                newName: "IX_documents_organizationId");

            migrationBuilder.RenameIndex(
                name: "IX_documents_document_type_id",
                table: "documents",
                newName: "IX_documents_documentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_document_types_documentTypeId",
                table: "documents",
                column: "documentTypeId",
                principalTable: "document_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_documents_organizations_organizationId",
                table: "documents",
                column: "organizationId",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
