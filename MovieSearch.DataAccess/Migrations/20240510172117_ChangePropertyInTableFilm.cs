using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSearch.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyInTableFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreIds",
                table: "Films",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreIds",
                table: "Films");
        }
    }
}
