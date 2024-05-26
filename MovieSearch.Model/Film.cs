using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MovieSearch.Model
{
    public class Film
    {
        [Key]
        [Required]
        public int Id { get; set; }
            
        public bool? Adult { get; set; }

        [Required]
        public string Title { get; set; }
        
        public ICollection<FilmGenre> FilmGenres { get; set; }
            
        public string? Backdrop_Path { get; set; }
            
        public string? Original_Language { get; set; }
            
        public string? Original_Title { get; set; }
            
        public string? Overview { get; set; }
            
        public decimal? Popularity { get; set; }

        public string? Poster_Path { get; set; }
        public DateTime? Release_Date { get; set; } 
            
        public decimal? Vote_Average { get; set; }

        [Required]
        public int Vote_Count { get; set; }
        
        public ICollection<Review> Reviews { get; set; }
    }
}