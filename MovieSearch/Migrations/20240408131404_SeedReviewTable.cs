using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSearch.Migrations
{
    /// <inheritdoc />
    public partial class SeedReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Headline", "SpoilersConsist", "TextReview" },
                values: new object[] { 1, "Beautiful Landscapes", false, "If that makes any sense. What I'm trying to say while pointing Aristotle's quote into a mirror, is that this is worth watching simply for all of the outstanding individual performances. There are many other reasons to tune in, but the acting clinic on parade here is a lot of fun." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
