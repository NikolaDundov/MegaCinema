namespace MegaCinema.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedMovieSeatModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Halls_HallId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "MovieActors");

            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_HallId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Tickets");

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Seats",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CinameId",
                table: "Halls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CinemaId",
                table: "Halls",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Genre",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Actors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_MovieId",
                table: "Genre",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieId",
                table: "Actors",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Movies_MovieId",
                table: "Genre",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Movies_MovieId",
                table: "Genre");

            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Genre_MovieId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CinameId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "CinemaId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MovieActors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActors", x => new { x.ActorId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.GenreId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_HallId",
                table: "Tickets",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActors_MovieId",
                table: "MovieActors",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId",
                table: "MovieGenres",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Halls_HallId",
                table: "Tickets",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
