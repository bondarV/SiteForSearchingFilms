using System.ComponentModel.DataAnnotations;
namespace MovieSearch.Model
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        
        public ICollection<FilmGenre> FilmGenres { get; set; }




    }
}
