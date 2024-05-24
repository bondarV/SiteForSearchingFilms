using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieSearch.Model;

public class FilmGenre
{
    public int FilmId { get; set; }
    [ForeignKey("FilmId")]
    public Film Film { get; set; }
    

    public int GenreId { get; set; }
    [ForeignKey("GenreId")]
    public Genre Genre { get; set; }
}