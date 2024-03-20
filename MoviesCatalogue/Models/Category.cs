using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesCatalogue.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Category name")]
        public string Name { get; set; }


    }
}
