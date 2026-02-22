using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class pp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentVerifications_Users_ReviewedByUserId",
                table: "StudentVerifications");

            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications");

            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_ReviewedByUserId",
                table: "StudentVerifications");

            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_Status",
                table: "StudentVerifications");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "StudentVerifications");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "StudentVerifications");

            migrationBuilder.DropColumn(
                name: "ReviewedByUserId",
                table: "StudentVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications");

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "StudentVerifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "StudentVerifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewedByUserId",
                table: "StudentVerifications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ParticipantId",
                table: "StudentVerifications",
                column: "ParticipantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_ReviewedByUserId",
                table: "StudentVerifications",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVerifications_Status",
                table: "StudentVerifications",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVerifications_Users_ReviewedByUserId",
                table: "StudentVerifications",
                column: "ReviewedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
