using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMovieIdKeyProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Movies_MoviesMovieId",
                table: "GenresMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesMovieId",
                table: "MoviesCinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Movies_MoviesMovieId",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "StarsMovies",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_StarsMovies_MoviesMovieId",
                table: "StarsMovies",
                newName: "IX_StarsMovies_MoviesId");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "MoviesCinemas",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesCinemas_MoviesMovieId",
                table: "MoviesCinemas",
                newName: "IX_MoviesCinemas_MoviesId");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "GenresMovies",
                newName: "MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_GenresMovies_MoviesMovieId",
                table: "GenresMovies",
                newName: "IX_GenresMovies_MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Movies_MoviesId",
                table: "GenresMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesId",
                table: "MoviesCinemas",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Movies_MoviesId",
                table: "StarsMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Movies_MoviesId",
                table: "GenresMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesId",
                table: "MoviesCinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Movies_MoviesId",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "StarsMovies",
                newName: "MoviesMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_StarsMovies_MoviesId",
                table: "StarsMovies",
                newName: "IX_StarsMovies_MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MoviesCinemas",
                newName: "MoviesMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesCinemas_MoviesId",
                table: "MoviesCinemas",
                newName: "IX_MoviesCinemas_MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "GenresMovies",
                newName: "MoviesMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_GenresMovies_MoviesId",
                table: "GenresMovies",
                newName: "IX_GenresMovies_MoviesMovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Movies_MoviesMovieId",
                table: "GenresMovies",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesMovieId",
                table: "MoviesCinemas",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Movies_MoviesMovieId",
                table: "StarsMovies",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
