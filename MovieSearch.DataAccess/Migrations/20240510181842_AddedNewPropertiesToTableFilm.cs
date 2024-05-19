using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSearch.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPropertiesToTableFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackdropPath",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "VoteCount",
                table: "Films",
                newName: "Vote_Count");

            migrationBuilder.RenameColumn(
                name: "VoteAverage",
                table: "Films",
                newName: "Vote_Average");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Films",
                newName: "Release_Date");

            migrationBuilder.RenameColumn(
                name: "PosterPath",
                table: "Films",
                newName: "Poster_Path");

            migrationBuilder.RenameColumn(
                name: "OriginalTitle",
                table: "Films",
                newName: "Original_Title");

            migrationBuilder.RenameColumn(
                name: "OriginalLanguage",
                table: "Films",
                newName: "Original_Language");

            migrationBuilder.RenameColumn(
                name: "GenreIds",
                table: "Films",
                newName: "Backdrop_Path");

            migrationBuilder.AddColumn<string>(
                name: "Genre_Ids",
                table: "Films",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre_Ids",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "Vote_Count",
                table: "Films",
                newName: "VoteCount");

            migrationBuilder.RenameColumn(
                name: "Vote_Average",
                table: "Films",
                newName: "VoteAverage");

            migrationBuilder.RenameColumn(
                name: "Release_Date",
                table: "Films",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "Poster_Path",
                table: "Films",
                newName: "PosterPath");

            migrationBuilder.RenameColumn(
                name: "Original_Title",
                table: "Films",
                newName: "OriginalTitle");

            migrationBuilder.RenameColumn(
                name: "Original_Language",
                table: "Films",
                newName: "OriginalLanguage");

            migrationBuilder.RenameColumn(
                name: "Backdrop_Path",
                table: "Films",
                newName: "GenreIds");

            migrationBuilder.AddColumn<string>(
                name: "BackdropPath",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Video",
                table: "Films",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
