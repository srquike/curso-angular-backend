using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursoAngular.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinTableForStarsAndMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Movies_MoviesId",
                table: "StarsMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Stars_CastId",
                table: "StarsMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StarsMovies",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "StarsMovies",
                newName: "StarId");

            migrationBuilder.RenameColumn(
                name: "CastId",
                table: "StarsMovies",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_StarsMovies_MoviesId",
                table: "StarsMovies",
                newName: "IX_StarsMovies_StarId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StarsMovies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Character",
                table: "StarsMovies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "StarsMovies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StarsMovies",
                table: "StarsMovies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StarsMovies_MovieId",
                table: "StarsMovies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Movies_MovieId",
                table: "StarsMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Stars_StarId",
                table: "StarsMovies",
                column: "StarId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Movies_MovieId",
                table: "StarsMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_StarsMovies_Stars_StarId",
                table: "StarsMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StarsMovies",
                table: "StarsMovies");

            migrationBuilder.DropIndex(
                name: "IX_StarsMovies_MovieId",
                table: "StarsMovies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StarsMovies");

            migrationBuilder.DropColumn(
                name: "Character",
                table: "StarsMovies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "StarsMovies");

            migrationBuilder.RenameColumn(
                name: "StarId",
                table: "StarsMovies",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "StarsMovies",
                newName: "CastId");

            migrationBuilder.RenameIndex(
                name: "IX_StarsMovies_StarId",
                table: "StarsMovies",
                newName: "IX_StarsMovies_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StarsMovies",
                table: "StarsMovies",
                columns: new[] { "CastId", "MoviesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Movies_MoviesId",
                table: "StarsMovies",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StarsMovies_Stars_CastId",
                table: "StarsMovies",
                column: "CastId",
                principalTable: "Stars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
