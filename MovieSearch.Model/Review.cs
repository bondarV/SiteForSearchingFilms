using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MovieSearch.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Назва рецензії")]
        [MaxLength(60, ErrorMessage = "Занадто велика назва")]
        public string Headline { get; set; }
        
        [DisplayName("Текст рецензії")]
        [Required]
        [MinLength(10, ErrorMessage = "Введіть більше 10 символів (включно з пробілами та розділовими знаками)")]
        [MaxLength(1000, ErrorMessage = "Введіть менше 1000 символів (включно з пробілами та розділовими знаками)")]
        public string TextReview { get; set; }
        
        [Required]
        [DisplayName("Чи наявні спойлери в рецензії?")]
        public bool SpoilersConsist { get; set; }
        
        public byte Rating { get; set; }

        public int FilmId { get; set; }
        
        [ValidateNever]
        public Film Film { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        

        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}