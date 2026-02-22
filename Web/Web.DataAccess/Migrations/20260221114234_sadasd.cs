using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class sadasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentStatus",
                table: "Participants");

            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "Participants",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "Participants");

            migrationBuilder.AddColumn<int>(
                name: "StudentStatus",
                table: "Participants",
                type: "integer",
                nullable: true);
        }
    }
}
