using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStarIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Stars_CastStarId",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "CastStarId",
                table: "StarsMovies",
                newName: "CastId");

            migrationBuilder.RenameColumn(
                name: "StarId",
                table: "Stars",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Stars_CastId",
                table: "StarsMovies",
                column: "CastId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Stars_CastId",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "CastId",
                table: "StarsMovies",
                newName: "CastStarId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stars",
                newName: "StarId");

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Stars_CastStarId",
                table: "StarsMovies",
                column: "CastStarId",
                principalTable: "Stars",
                principalColumn: "StarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
