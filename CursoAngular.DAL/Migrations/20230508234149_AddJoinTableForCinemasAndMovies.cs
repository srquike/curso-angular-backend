using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinTableForCinemasAndMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasId",
                table: "MoviesCinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesId",
                table: "MoviesCinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviesCinemas",
                table: "MoviesCinemas");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "MoviesCinemas",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "CinemasId",
                table: "MoviesCinemas",
                newName: "CinemaId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesCinemas_MoviesId",
                table: "MoviesCinemas",
                newName: "IX_MoviesCinemas_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MoviesCinemas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviesCinemas",
                table: "MoviesCinemas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesCinemas_CinemaId",
                table: "MoviesCinemas",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemaId",
                table: "MoviesCinemas",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Movies_MovieId",
                table: "MoviesCinemas",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemaId",
                table: "MoviesCinemas");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Movies_MovieId",
                table: "MoviesCinemas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviesCinemas",
                table: "MoviesCinemas");

            migrationBuilder.DropIndex(
                name: "IX_MoviesCinemas_CinemaId",
                table: "MoviesCinemas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MoviesCinemas");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "MoviesCinemas",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "MoviesCinemas",
                newName: "CinemasId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviesCinemas_MovieId",
                table: "MoviesCinemas",
                newName: "IX_MoviesCinemas_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviesCinemas",
                table: "MoviesCinemas",
                columns: new[] { "CinemasId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasId",
                table: "MoviesCinemas",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Movies_MoviesId",
                table: "MoviesCinemas",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
