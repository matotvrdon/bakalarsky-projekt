using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MoveStudentVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    OriginalFileName = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ParticipantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentVerifications_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.Sql("""
                INSERT INTO "StudentVerifications" (
                    "Status",
                    "FilePath",
                    "OriginalFileName",
                    "ContentType",
                    "UploadedAt",
                    "ParticipantId"
                )
                SELECT
                    COALESCE("StudentStatus", 0),
                    "StudentVerificationFilePath",
                    "StudentVerificationOriginalFileName",
                    "StudentVerificationContentType",
                    "StudentVerificationUploadedAt",
                    "Id"
                FROM "Participants"
                WHERE "StudentStatus" IS NOT NULL
                   OR "StudentVerificationFilePath" IS NOT NULL
                   OR "StudentVerificationOriginalFileName" IS NOT NULL
                   OR "StudentVerificationContentType" IS NOT NULL
                   OR "StudentVerificationUploadedAt" IS NOT NULL;
                """);

            migrationBuilder.DropColumn(
                name: "StudentStatus",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudentVerificationContentType",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudentVerificationFilePath",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudentVerificationOriginalFileName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudentVerificationUploadedAt",
                table: "Participants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentStatus",
                table: "Participants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentVerificationContentType",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentVerificationFilePath",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentVerificationOriginalFileName",
                table: "Participants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StudentVerificationUploadedAt",
                table: "Participants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE "Participants" p
                SET
                    "StudentStatus" = sv."Status",
                    "StudentVerificationFilePath" = sv."FilePath",
                    "StudentVerificationOriginalFileName" = sv."OriginalFileName",
                    "StudentVerificationContentType" = sv."ContentType",
                    "StudentVerificationUploadedAt" = sv."UploadedAt"
                FROM "StudentVerifications" sv
                WHERE sv."ParticipantId" = p."Id";
                """);

            migrationBuilder.DropTable(
                name: "StudentVerifications");
        }
    }
}
