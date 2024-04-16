using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSearch.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProperiesInTableToEf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShort",
                table: "Genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShort",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
