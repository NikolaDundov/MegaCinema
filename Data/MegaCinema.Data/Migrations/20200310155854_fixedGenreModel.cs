using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaCinema.Data.Migrations
{
    public partial class fixedGenreModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Movies_MovieId",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_MovieId",
                table: "Genres",
                newName: "IX_Genres_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "GenreType",
                table: "Genres",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Movies_MovieId",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "GenreType",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameIndex(
                name: "IX_Genres_MovieId",
                table: "Genre",
                newName: "IX_Genre_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Genre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Movies_MovieId",
                table: "Genre",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
