using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesCatalogue.Models;

namespace MoviesCatalogue.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Action"

                },
                new Category
                {
                    Id = 2,
                    Name = "Sc-Fi"
                },
                new Category
                {
                    Id = 3,
                    Name = "Thriller"
                });
            modelbuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Interstellar",
                    Description = "Space travel and time stuff",
                    Director = "Christopher Nolan",
                    Rating = 9.12,
                    CategoryId = 2,
                    ImageUrl = ""

                },
                new Movie
                {
                     Id = 2,
                     Title = "Drive",
                     Description = "I drive",
                     Director = "Nicolas Winding",
                     Rating = 7.45,
                     CategoryId = 1,
                    ImageUrl = ""

                },
                new Movie
                {
                    Id = 3,
                    Title = "Fight club",
                    Description = "We don't talk about it.",
                    Director = "David Fincher",
                    Rating = 8.93,
                    CategoryId = 3,
                    ImageUrl=""

                }
            );
        }
    }
}
