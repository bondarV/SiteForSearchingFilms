using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Models{

public class Review
{
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Headline { get; set; }
        [DisplayName("Текст рецензії")]
        [Required]
        public string TextReview { get; set;}
        [Required]
        [DisplayName("Чи наявні спойлери в рецензії?")]
        public bool SpoilersConsist { get; set;}
        
    }
}