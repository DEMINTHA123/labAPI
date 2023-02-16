using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labAPI.Migrations
{
    /// <inheritdoc />
    public partial class addLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Labs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Labs");
        }
    }
}
