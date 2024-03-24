using Microsoft.EntityFrameworkCore;
using MoviesCatalogue.Data;
using MoviesCatalogue.Models;
using MoviesCatalogue.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MoviesCatalogue.Tests.Repository
{
    [TestFixture]
    public class RepositoryTests
    {
        private Repository<Movie> _repository;
        private Mock<ApplicationContext> _mockContext;
        private Mock<DbSet<Movie>> _mockDbSet;

        [SetUp]
        public void Setup()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            }.AsQueryable();

            _mockDbSet = new Mock<DbSet<Movie>>();

            _mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Provider).Returns(movies.Provider);
            _mockDbSet.As<IQueryable<Movie>>().Setup(m => m.Expression).Returns(movies.Expression);
            _mockDbSet.As<IQueryable<Movie>>().Setup(m => m.ElementType).Returns(movies.ElementType);
            _mockDbSet.As<IQueryable<Movie>>().Setup(m => m.GetEnumerator()).Returns(movies.GetEnumerator());

            _mockContext = new Mock<ApplicationContext>();
            _mockContext.Setup(x => x.Set<Movie>()).Returns(_mockDbSet.Object);

            _repository = new Repository<Movie>(_mockContext.Object);
        }

        [Test]
        public void Add_Entity_AddsSuccessfully()
        {
            var movie = new Movie();

            _repository.Add(movie);

            _mockDbSet.Verify(x => x.Add(movie), Times.Once);
        }

        [Test]
        public void Delete_Entity_RemovesSuccessfully()
        {
            var movie = new Movie();

            _repository.Delete(movie);

            _mockDbSet.Verify(x => x.Remove(movie), Times.Once);
        }

        [Test]
        public void Get_WithFilter_ReturnsEntity()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            }.AsQueryable();

            Expression<Func<Movie, bool>> filter = m => m.Id == 1;

            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.Provider).Returns(movies.Provider);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.Expression).Returns(movies.Expression);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.ElementType).Returns(movies.ElementType);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.GetEnumerator()).Returns(movies.GetEnumerator());

            _mockContext.Setup(x => x.Set<Movie>()).Returns(_mockDbSet.Object);

            var result = _repository.Get(filter);

            Assert.IsNotNull(result);
            Assert.That(result.Title, Is.EqualTo("Movie 1"));
        }

        [Test]
        public void GetAll_ReturnsAllEntities()
        {
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1" },
                new Movie { Id = 2, Title = "Movie 2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.Provider).Returns(movies.Provider);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.Expression).Returns(movies.Expression);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.ElementType).Returns(movies.ElementType);
            _mockDbSet.As<IQueryable<Movie>>().Setup(x => x.GetEnumerator()).Returns(movies.GetEnumerator());

            _mockContext.Setup(x => x.Set<Movie>()).Returns(_mockDbSet.Object);

            var result = _repository.GetAll();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Movie 1"));
            Assert.That(result.Last().Title, Is.EqualTo("Movie 2"));
        }
    }
}
