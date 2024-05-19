using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MovieSearch.Model.Models;

namespace MovieSearch.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Review>().HasData(new Review
            {
                Id = 1, Headline = "Beautiful Landscapes",
                TextReview =
                    "If that makes any sense. What I'm trying to say while pointing Aristotle's quote into a mirror, is that this is worth watching simply for all of the outstanding individual performances. There are many other reasons to tune in, but the acting clinic on parade here is a lot of fun.",
                SpoilersConsist = false
            });
            modelBuilder.Entity<FilmGenre>()
                .HasKey(fg => new { fg.FilmId, fg.GenreId }); // Define composite primary key for FilmGenre

            modelBuilder.Entity<FilmGenre>()
                .HasOne(fg => fg.Film)
                .WithMany(f => f.FilmGenres) // One Film has many FilmGenres
                .HasForeignKey(fg => fg.FilmId); // Foreign key for Film

            modelBuilder.Entity<FilmGenre>()
                .HasOne(fg => fg.Genre)
                .WithMany(g => g.FilmGenres) // One Genre has many FilmGenres
                .HasForeignKey(fg => fg.GenreId); // Foreign key for Genre
        }
        
    }
    
}
    