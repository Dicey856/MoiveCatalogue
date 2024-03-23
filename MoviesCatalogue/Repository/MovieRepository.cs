using MoviesCatalogue.Data;
using MoviesCatalogue.Models;
using MoviesCatalogue.Repository.IRepository;

namespace MoviesCatalogue.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private ApplicationContext _db;
        public MovieRepository(ApplicationContext db) : base(db)
        {
              _db = db; 
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Movie obj)
        {
            _db.Movies.Update(obj);
        }
    }
}
