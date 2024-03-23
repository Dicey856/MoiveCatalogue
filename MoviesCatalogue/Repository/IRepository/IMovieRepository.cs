using MoviesCatalogue.Models;

namespace MoviesCatalogue.Repository.IRepository
{
    public interface IMovieRepository : IRepository<Movie>
    {
        void Update(Movie obj);
        void Save();
    }
}
