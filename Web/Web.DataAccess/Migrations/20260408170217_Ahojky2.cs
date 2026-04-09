using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ahojky2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConferenceImportantDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConferenceSettings",
                table: "ConferenceSettings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ConferenceSettings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConferenceSettings",
                table: "ConferenceSettings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImportantDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NormalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ImportantDatesStatus = table.Column<int>(type: "integer", nullable: false),
                    ConferenceSettingsId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportantDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportantDates_ConferenceSettings_ConferenceSettingsId",
                        column: x => x.ConferenceSettingsId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceSettings_ConferenceId",
                table: "ConferenceSettings",
                column: "ConferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportantDates_ConferenceSettingsId",
                table: "ImportantDates",
                column: "ConferenceSettingsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportantDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConferenceSettings",
                table: "ConferenceSettings");

            migrationBuilder.DropIndex(
                name: "IX_ConferenceSettings_ConferenceId",
                table: "ConferenceSettings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ConferenceSettings",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConferenceSettings",
                table: "ConferenceSettings",
                column: "ConferenceId");

            migrationBuilder.CreateTable(
                name: "ConferenceImportantDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false),
                    ImportantDatesStatus = table.Column<int>(type: "integer", nullable: false),
                    NormalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceImportantDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceImportantDates_ConferenceSettings_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "ConferenceSettings",
                        principalColumn: "ConferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceImportantDates_ConferenceId",
                table: "ConferenceImportantDates",
                column: "ConferenceId");
        }
    }
}
