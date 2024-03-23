using MoviesCatalogue.Models;

namespace MoviesCatalogue.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);

    }
}
