using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Areas.Admin.Controllers.Dto
{
    public class FilmDto
    {

        public int Id { get; set; }
            
        public bool? Adult { get; set; }

        [Required]
        public string Title { get; set; }
           
        public ICollection<int> Genre_Ids { get; set; }
            
        public string? Backdrop_Path { get; set; }
            
        public string? Original_Language { get; set; }
            
        public string? Original_Title { get; set; }
            
        public string? Overview { get; set; }
            
        public decimal? Popularity { get; set; }

        public string? Poster_Path { get; set; }
                
        [DataType(DataType.Date)]
        public DateTime? Release_Date { get; set; }
            
        public decimal? Vote_Average { get; set; }

        [Required]
        public int Vote_Count { get; set; }
    }
}