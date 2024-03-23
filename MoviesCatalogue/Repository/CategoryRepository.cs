using MoviesCatalogue.Data;
using MoviesCatalogue.Models;
using MoviesCatalogue.Repository.IRepository;
using System.Linq.Expressions;

namespace MoviesCatalogue.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationContext _db;
        public CategoryRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
