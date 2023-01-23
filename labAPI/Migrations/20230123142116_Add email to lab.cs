using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labAPI.Migrations
{
    /// <inheritdoc />
    public partial class Addemailtolab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Labs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Labs");
        }
    }
}
