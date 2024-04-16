using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MovieSearch.Models;

namespace MovieSearch.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { 
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Adventure" },
                new Genre { Id = 3, Name = "Animation" });
            modelBuilder.Entity<Review>().HasData(new Review
            {
                Id = 1, Headline = "Beautiful Landscapes",
                TextReview =
                    "If that makes any sense. What I'm trying to say while pointing Aristotle's quote into a mirror, is that this is worth watching simply for all of the outstanding individual performances. There are many other reasons to tune in, but the acting clinic on parade here is a lot of fun.",
                SpoilersConsist = false
            });
        }
    }
}
    