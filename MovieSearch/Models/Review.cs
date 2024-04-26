using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieSearch.Models{

public class Review
{
    [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Назва рецензії")]
        [MaxLength(60,ErrorMessage = "Занадто велика назва")]
        public string Headline { get; set; }
        [DisplayName("Текст рецензії")]
        [Required]
        [MinLength(10,ErrorMessage ="Введіть більше 10 символів(включно з пробілами та розділовими знаками)")]
        [MaxLength(1000,ErrorMessage = "Введіть менше 10 символів(включно з пробілами та розділовими знаками)")]
        public string TextReview { get; set;}
        [Required]
        [DisplayName("Чи наявні спойлери в рецензії?")]
        public bool SpoilersConsist { get; set;}
        public byte Rating { get; set; }
    
}
}