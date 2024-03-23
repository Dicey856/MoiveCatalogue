using MoviesCatalogue.Data;
using MoviesCatalogue.Repository.IRepository;

namespace MoviesCatalogue.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;
        public ICategoryRepository Category { get; private set; }

        public IMovieRepository Movie { get; private set; }
        public UnitOfWork(ApplicationContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Movie = new MovieRepository(_db);
        }
        
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
