using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labAPI.Migrations
{
    /// <inheritdoc />
    public partial class addingnewfieldstoequipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Equipment");
        }
    }
}
