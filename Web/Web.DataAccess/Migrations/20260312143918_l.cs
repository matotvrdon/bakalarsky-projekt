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
            migrationBuilder.AddColumn<bool>(
                name: "IsPresenting",
                table: "Participants",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPresenting",
                table: "Participants");
        }
    }
}
