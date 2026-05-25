using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddConferencePublishAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Conferences",
                newName: "IsPublished");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Conferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Conferences");

            migrationBuilder.RenameColumn(
                name: "IsPublished",
                table: "Conferences",
                newName: "IsActive");
        }
    }
}
