using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParticipantStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    RequiresApproval = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantStatuses_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantStatusAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParticipantId = table.Column<int>(type: "integer", nullable: false),
                    ParticipantStatusId = table.Column<int>(type: "integer", nullable: false),
                    ApprovalState = table.Column<int>(type: "integer", nullable: false),
                    FileManagerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantStatusAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipantStatusAssignments_FileManagers_FileManagerId",
                        column: x => x.FileManagerId,
                        principalTable: "FileManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ParticipantStatusAssignments_ParticipantStatuses_Participan~",
                        column: x => x.ParticipantStatusId,
                        principalTable: "ParticipantStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParticipantStatusAssignments_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantStatusAssignments_FileManagerId",
                table: "ParticipantStatusAssignments",
                column: "FileManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantStatusAssignments_ParticipantId_ParticipantStatu~",
                table: "ParticipantStatusAssignments",
                columns: new[] { "ParticipantId", "ParticipantStatusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantStatusAssignments_ParticipantStatusId",
                table: "ParticipantStatusAssignments",
                column: "ParticipantStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantStatuses_ConferenceSettingsId",
                table: "ParticipantStatuses",
                column: "ConferenceSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantStatusAssignments");

            migrationBuilder.DropTable(
                name: "ParticipantStatuses");
        }
    }
}
