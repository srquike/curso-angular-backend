using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinTableForGenresAndMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Genres_GenresId",
                table: "GenresMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Movies_MoviesId",
                table: "GenresMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenresMovies",
                table: "GenresMovies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "GenresMovies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenresMovies",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GenresMovies_MoviesId",
                table: "GenresMovies",
                newName: "IX_GenresMovies_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GenresMovies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenresMovies",
                table: "GenresMovies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GenresMovies_GenreId",
                table: "GenresMovies",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Genres_GenreId",
                table: "GenresMovies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Movies_MovieId",
                table: "GenresMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Genres_GenreId",
                table: "GenresMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresMovies_Movies_MovieId",
                table: "GenresMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenresMovies",
                table: "GenresMovies");

            migrationBuilder.DropIndex(
                name: "IX_GenresMovies_GenreId",
                table: "GenresMovies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GenresMovies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "GenresMovies",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "GenresMovies",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_GenresMovies_MovieId",
                table: "GenresMovies",
                newName: "IX_GenresMovies_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenresMovies",
                table: "GenresMovies",
                columns: new[] { "GenresId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Genres_GenresId",
                table: "GenresMovies",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMovies_Movies_MoviesId",
                table: "GenresMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
