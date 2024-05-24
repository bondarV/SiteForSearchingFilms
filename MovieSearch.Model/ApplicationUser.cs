using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieSearch.Model;

public class ApplicationUser:IdentityUser
{
    [Required]
    public string Name { get; set; }
    
}