using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class nhjnj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSettings_Conferences_ConferenceId",
                table: "ConferenceSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conferences_ConferenceId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Conferences_ConferenceId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences");

            migrationBuilder.RenameTable(
                name: "Conferences",
                newName: "Conference");

            migrationBuilder.RenameColumn(
                name: "RegistrationType",
                table: "Participants",
                newName: "ConferenceEntryId");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "FoodOptions",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "BookingOptions",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conference",
                table: "Conference",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConferenceEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConferenceId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConferenceEntries_Conference_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ConferenceEntryId",
                table: "Participants",
                column: "ConferenceEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceEntries_ConferenceId",
                table: "ConferenceEntries",
                column: "ConferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConferenceSettings_Conference_ConferenceId",
                table: "ConferenceSettings",
                column: "ConferenceId",
                principalTable: "Conference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_ConferenceEntries_ConferenceEntryId",
                table: "Participants",
                column: "ConferenceEntryId",
                principalTable: "ConferenceEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConferenceSettings_Conference_ConferenceId",
                table: "ConferenceSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_ConferenceEntries_ConferenceEntryId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Conference_ConferenceId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Conference_ConferenceId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "ConferenceEntries");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ConferenceEntryId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conference",
                table: "Conference");

            migrationBuilder.RenameTable(
                name: "Conference",
                newName: "Conferences");

            migrationBuilder.RenameColumn(
                name: "ConferenceEntryId",
                table: "Participants",
                newName: "RegistrationType");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "FoodOptions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "BookingOptions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conferences",
                table: "Conferences",
                column: "Id");

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
    }
}
