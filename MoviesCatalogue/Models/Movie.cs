using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesCatalogue.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Required]
        public string Director { get; set; }


        public int CategoryId {  get; set; }
        
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [Range(0, 10)]
        public double Rating { get; set; }

        [ValidateNever]
        public string? ImageUrl {  get; set; }
    }
}
