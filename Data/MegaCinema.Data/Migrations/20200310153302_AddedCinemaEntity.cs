using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class AddedCinemaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Projections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    OpenHour = table.Column<DateTime>(nullable: false),
                    ClosingHour = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projections_CinemaId",
                table: "Projections",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projections_Cinemas_CinemaId",
                table: "Projections",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projections_Cinemas_CinemaId",
                table: "Projections");

            migrationBuilder.DropTable(
                name: "Cinemas");

            migrationBuilder.DropIndex(
                name: "IX_Projections_CinemaId",
                table: "Projections");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Projections");
        }
    }
}
