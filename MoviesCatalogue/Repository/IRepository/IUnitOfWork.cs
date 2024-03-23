namespace MoviesCatalogue.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IMovieRepository Movie { get; }

        void Save();
    }
}
