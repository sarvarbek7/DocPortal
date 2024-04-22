using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "DocumentType",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Title = table.Column<string>(type: "character varying(1023)", maxLength: 1023, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_DocumentType", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Organization",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Title = table.Column<string>(type: "character varying(1023)", maxLength: 1023, nullable: false),
            PrimaryOrganizationId = table.Column<int>(type: "integer", nullable: true),
            PhysicalIdentity = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            Details = table.Column<string>(type: "text", nullable: true),
            CreatedBy = table.Column<int>(type: "integer", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
            UpdatedBy = table.Column<int>(type: "integer", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
            IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Organization", x => x.Id);
            table.ForeignKey(
                      name: "FK_Organization_Organization_PrimaryOrganizationId",
                      column: x => x.PrimaryOrganizationId,
                      principalTable: "Organization",
                      principalColumn: "Id");
          });

      migrationBuilder.CreateTable(
          name: "User",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            PhysicalIdentity = table.Column<string>(type: "char(14)", nullable: false),
            Firstname = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
            LastName = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
            JobPosition = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: false),
            Login = table.Column<string>(type: "character varying(127)", maxLength: 127, nullable: true),
            PasswordHash = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: true),
            CreatedBy = table.Column<int>(type: "integer", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
            UpdatedBy = table.Column<int>(type: "integer", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
            IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_User", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Document",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Title = table.Column<string>(type: "character varying(1023)", maxLength: 1023, nullable: false),
            RegisteredNumber = table.Column<string>(type: "character varying(63)", maxLength: 63, nullable: false),
            RegisteredDate = table.Column<DateOnly>(type: "date", nullable: false),
            IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
            StoragePath = table.Column<string>(type: "text", nullable: false),
            OrganizationId = table.Column<int>(type: "integer", nullable: false),
            DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
            CreatedBy = table.Column<int>(type: "integer", nullable: false),
            UpdatedBy = table.Column<int>(type: "integer", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "timestamp", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Document", x => x.Id);
            table.ForeignKey(
                      name: "FK_Document_DocumentType_DocumentTypeId",
                      column: x => x.DocumentTypeId,
                      principalTable: "DocumentType",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Document_Organization_OrganizationId",
                      column: x => x.OrganizationId,
                      principalTable: "Organization",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "UserRole",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            UserId = table.Column<int>(type: "integer", nullable: false),
            OrganizationId = table.Column<int>(type: "integer", nullable: false),
            Role = table.Column<string>(type: "character varying(31)", maxLength: 31, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_UserRole", x => x.Id);
            table.ForeignKey(
                      name: "FK_UserRole_Organization_OrganizationId",
                      column: x => x.OrganizationId,
                      principalTable: "Organization",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_UserRole_User_UserId",
                      column: x => x.UserId,
                      principalTable: "User",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Document_DocumentTypeId",
          table: "Document",
          column: "DocumentTypeId");

      migrationBuilder.CreateIndex(
          name: "IX_Document_OrganizationId",
          table: "Document",
          column: "OrganizationId");

      migrationBuilder.CreateIndex(
          name: "IX_Organization_PrimaryOrganizationId",
          table: "Organization",
          column: "PrimaryOrganizationId");

      migrationBuilder.CreateIndex(
          name: "IX_UserRole_OrganizationId",
          table: "UserRole",
          column: "OrganizationId");

      migrationBuilder.CreateIndex(
          name: "IX_UserRole_UserId",
          table: "UserRole",
          column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Document");

      migrationBuilder.DropTable(
          name: "UserRole");

      migrationBuilder.DropTable(
          name: "DocumentType");

      migrationBuilder.DropTable(
          name: "Organization");

      migrationBuilder.DropTable(
          name: "User");
    }
  }
}
