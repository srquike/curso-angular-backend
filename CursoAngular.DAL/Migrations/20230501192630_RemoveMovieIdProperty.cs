using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMovieIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasCinemaId",
                table: "MoviesCinemas");

            migrationBuilder.RenameColumn(
                name: "CinemasCinemaId",
                table: "MoviesCinemas",
                newName: "CinemasId");

            migrationBuilder.RenameColumn(
                name: "CinemaId",
                table: "Cinemas",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasId",
                table: "MoviesCinemas",
                column: "CinemasId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasId",
                table: "MoviesCinemas");

            migrationBuilder.RenameColumn(
                name: "CinemasId",
                table: "MoviesCinemas",
                newName: "CinemasCinemaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cinemas",
                newName: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesCinemas_Cinemas_CinemasCinemaId",
                table: "MoviesCinemas",
                column: "CinemasCinemaId",
                principalTable: "Cinemas",
                principalColumn: "CinemaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
