using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class ProjectionProjectionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectionId",
                table: "Seats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProjectionId",
                table: "Seats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
