using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class l : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications");

            migrationBuilder.DropIndex(
                name: "IX_Participants_UserId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "StudentVerificationId",
                table: "Participants");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId_ConferenceId",
                table: "Participants",
                columns: new[] { "UserId", "ConferenceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications");

            migrationBuilder.DropIndex(
                name: "IX_Participants_UserId_ConferenceId",
                table: "Participants");

            migrationBuilder.AddColumn<int>(
                name: "StudentVerificationId",
                table: "Participants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");
        }
    }
}
