using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace labAPI.Migrations
{
    /// <inheritdoc />
    public partial class changingdatatypeofphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_PhotoId",
                table: "Equipment",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Photos_PhotoId",
                table: "Equipment",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Photos_PhotoId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_PhotoId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Equipment");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
