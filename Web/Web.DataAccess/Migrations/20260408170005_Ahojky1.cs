using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Ahojky1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceImportantDates_Conferences_ConferenceId",
                table: "ConferenceImportantDates");

            migrationBuilder.CreateTable(
                name: "ConferenceSettings",
                columns: table => new
                {
                    ConferenceId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceSettings", x => x.ConferenceId);
                    table.ForeignKey(
                        name: "FK_ConferenceSettings_Conferences_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceImportantDates_ConferenceSettings_ConferenceId",
                table: "ConferenceImportantDates",
                column: "ConferenceId",
                principalTable: "ConferenceSettings",
                principalColumn: "ConferenceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceImportantDates_ConferenceSettings_ConferenceId",
                table: "ConferenceImportantDates");

            migrationBuilder.DropTable(
                name: "ConferenceSettings");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceImportantDates_Conferences_ConferenceId",
                table: "ConferenceImportantDates",
                column: "ConferenceId",
                principalTable: "Conferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
