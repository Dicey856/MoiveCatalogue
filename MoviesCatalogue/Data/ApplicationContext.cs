using Microsoft.EntityFrameworkCore;
using MoviesCatalogue.Models;

namespace MoviesCatalogue.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        //public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            /*modelbuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action" },
                new Category { Id = 2, Name = "Horror" },
                new Category { Id = 3, Name = "Comedy" }
            );*/
        }
    }
}
