using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class uuuuu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceEntries_Conference_ConferenceId",
                table: "ConferenceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSettings_Conference_ConferenceId",
                table: "ConferenceSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conference_ConferenceId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Conference_ConferenceId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conference",
                table: "Conference");

            migrationBuilder.RenameTable(
                name: "Conference",
                newName: "Conferences");

            migrationBuilder.RenameColumn(
                name: "ConferenceId",
                table: "ConferenceEntries",
                newName: "ConferenceSettingsId");

            migrationBuilder.RenameIndex(
                name: "IX_ConferenceEntries_ConferenceId",
                table: "ConferenceEntries",
                newName: "IX_ConferenceEntries_ConferenceSettingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProgramDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDays_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramDayId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Speaker = table.Column<string>(type: "text", nullable: true),
                    Chair = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramItems_ProgramDays_ProgramDayId",
                        column: x => x.ProgramDayId,
                        principalTable: "ProgramDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramItemId = table.Column<int>(type: "integer", nullable: false),
                    SessionName = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Chair = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramSessions_ProgramItems_ProgramItemId",
                        column: x => x.ProgramItemId,
                        principalTable: "ProgramItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramPresentations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramSessionId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Authors = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramPresentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramPresentations_ProgramSessions_ProgramSessionId",
                        column: x => x.ProgramSessionId,
                        principalTable: "ProgramSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDays_ConferenceSettingsId",
                table: "ProgramDays",
                column: "ConferenceSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramItems_ProgramDayId",
                table: "ProgramItems",
                column: "ProgramDayId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramPresentations_ProgramSessionId",
                table: "ProgramPresentations",
                column: "ProgramSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSessions_ProgramItemId",
                table: "ProgramSessions",
                column: "ProgramItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceEntries_ConferenceSettings_ConferenceSettingsId",
                table: "ConferenceEntries",
                column: "ConferenceSettingsId",
                principalTable: "ConferenceSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSettings_Conferences_ConferenceId",
                table: "ConferenceSettings",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Conferences_ConferenceId",
                table: "Participants",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Conferences_ConferenceId",
                table: "Submissions",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceEntries_ConferenceSettings_ConferenceSettingsId",
                table: "ConferenceEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSettings_Conferences_ConferenceId",
                table: "ConferenceSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conferences_ConferenceId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Conferences_ConferenceId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "ProgramPresentations");

            migrationBuilder.DropTable(
                name: "ProgramSessions");

            migrationBuilder.DropTable(
                name: "ProgramItems");

            migrationBuilder.DropTable(
                name: "ProgramDays");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences");

            migrationBuilder.RenameTable(
                name: "Conferences",
                newName: "Conference");

            migrationBuilder.RenameColumn(
                name: "ConferenceSettingsId",
                table: "ConferenceEntries",
                newName: "ConferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_ConferenceEntries_ConferenceSettingsId",
                table: "ConferenceEntries",
                newName: "IX_ConferenceEntries_ConferenceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conference",
                table: "Conference",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceEntries_Conference_ConferenceId",
                table: "ConferenceEntries",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSettings_Conference_ConferenceId",
                table: "ConferenceSettings",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Conference_ConferenceId",
                table: "Participants",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Conference_ConferenceId",
                table: "Submissions",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
