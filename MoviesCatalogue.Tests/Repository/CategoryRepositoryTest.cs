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
    public class CategoryRepositoryTests
    {
        private Repository<Category> _repository;
        private Mock<ApplicationContext> _mockContext;
        private Mock<DbSet<Category>> _mockDbSet;

        [SetUp]
        public void Setup()
        {
            var categories = new List<Category>
            {
                new Category { Id = 11, Name = "Category 1" },
                new Category { Id = 15, Name = "Category 2" }
            }.AsQueryable();

            _mockDbSet = new Mock<DbSet<Category>>();

            _mockDbSet.As<IQueryable<Category>>().Setup(m => m.Provider).Returns(categories.Provider);
            _mockDbSet.As<IQueryable<Category>>().Setup(m => m.Expression).Returns(categories.Expression);
            _mockDbSet.As<IQueryable<Category>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            _mockDbSet.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

            _mockContext = new Mock<ApplicationContext>();
            _mockContext.Setup(x => x.Set<Category>()).Returns(_mockDbSet.Object);

            _repository = new Repository<Category>(_mockContext.Object);
        }

        [Test]
        public void Add_Entity_AddsSuccessfully()
        {
            var category = new Category();

            _repository.Add(category);

            _mockDbSet.Verify(x => x.Add(category), Times.Once);
        }

        [Test]
        public void Delete_Entity_RemovesSuccessfully()
        {
            var category = new Category();

            _repository.Delete(category);

            _mockDbSet.Verify(x => x.Remove(category), Times.Once);
        }

        [Test]
        public void Get_WithFilter_ReturnsEntity()
        {
            var categorys = new List<Category>
            {
                new Category { Id = 1, Name = "category 1" },
                new Category { Id = 2, Name = "category 2" }
            }.AsQueryable();

            Expression<Func<Category, bool>> filter = m => m.Id == 1;

            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.Provider).Returns(categorys.Provider);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.Expression).Returns(categorys.Expression);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.ElementType).Returns(categorys.ElementType);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.GetEnumerator()).Returns(categorys.GetEnumerator());

            _mockContext.Setup(x => x.Set<Category>()).Returns(_mockDbSet.Object);

            var result = _repository.Get(filter);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("category 1"));
        }

        [Test]
        public void GetAll_ReturnsAllEntities()
        {
            var categorys = new List<Category>
            {
                new Category { Id = 1, Name = "category 1" },
                new Category { Id = 2, Name = "category 2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.Provider).Returns(categorys.Provider);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.Expression).Returns(categorys.Expression);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.ElementType).Returns(categorys.ElementType);
            _mockDbSet.As<IQueryable<Category>>().Setup(x => x.GetEnumerator()).Returns(categorys.GetEnumerator());

            _mockContext.Setup(x => x.Set<Category>()).Returns(_mockDbSet.Object);

            var result = _repository.GetAll();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("category 1"));
            Assert.That(result.Last().Name, Is.EqualTo("category 2"));
        }
    }
}
