using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class AddImagePropertyToCinema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cinemas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cinemas");
        }
    }
}
