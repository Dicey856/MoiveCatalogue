using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalogue.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }

    }
}
