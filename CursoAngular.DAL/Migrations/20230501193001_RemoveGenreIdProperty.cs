using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenreIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Genres_GenresGenreId",
                table: "GenresMovies");

            migrationBuilder.RenameColumn(
                name: "GenresGenreId",
                table: "GenresMovies",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Genres_GenresId",
                table: "GenresMovies",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Genres_GenresId",
                table: "GenresMovies");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenresMovies",
                newName: "GenresGenreId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Genres_GenresGenreId",
                table: "GenresMovies",
                column: "GenresGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
