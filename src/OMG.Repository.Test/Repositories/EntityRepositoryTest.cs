using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OMG.Domain.Base;
using OMG.Repository;
using OMG.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace OMG.Repository.Test.Repositories
{
    public class EntityRepositoryTest
    {
        private readonly TestOMGDbContext _context;
        private readonly EntityRepository<TestEntity> _repository;

        public EntityRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<TestOMGDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TestOMGDbContext(options);
            _repository = new EntityRepository<TestEntity>(_context);
        }

        [Fact]
        public async Task Create_ShouldAddEntityToContext()
        {
            // Arrange
            var entity = new TestEntity { Name = "Test Entity" };

            // Act
            var result = await _repository.Create(entity);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Test Entity");
            _context.Set<TestEntity>().Should().ContainSingle(e => e.Name == "Test Entity");
        }

        [Fact]
        public async Task Delete_ShouldMarkEntityAsDeleted()
        {
            // Arrange
            var entity = new TestEntity { Name = "To Be Deleted" };
            await _repository.Create(entity);

            // Act
            var result = await _repository.Delete(entity.Id);

            // Assert
            result.Should().BeTrue();
            var deletedEntity = await _repository.Get(entity.Id);
            deletedEntity.IsDeleted.Should().BeTrue();
            deletedEntity.DeletedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task Get_ShouldReturnEntityById()
        {
            // Arrange
            var entity = new TestEntity { Name = "Find Me" };
            await _repository.Create(entity);

            // Act
            var result = await _repository.Get(entity.Id);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Find Me");
        }

        [Fact]
        public async Task Get_ShouldReturnEntityByPredicate()
        {
            // Arrange
            var entity = new TestEntity { Name = "Find By Predicate" };
            await _repository.Create(entity);

            // Act
            var result = await _repository.Get(e => e.Name == "Find By Predicate");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Find By Predicate");
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entity1 = new TestEntity { Name = "Entity 1" };
            var entity2 = new TestEntity { Name = "Entity 2" };
            await _repository.Create(entity1);
            await _repository.Create(entity2);

            // Act
            var result = await _repository.GetAll();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEntitiesMatchingPredicate()
        {
            // Arrange
            var entity1 = new TestEntity { Name = "Matching Entity" };
            var entity2 = new TestEntity { Name = "Non-Matching Entity" };
            await _repository.Create(entity1);
            await _repository.Create(entity2);

            // Act
            var result = await _repository.GetAll(e => e.Name.Contains("Matching"));

            // Assert
            result.Should().ContainSingle(e => e.Name == "Matching Entity");
        }

        [Fact]
        public async Task Update_ShouldModifyEntity()
        {
            // Arrange
            var entity = new TestEntity { Name = "Original Name" };
            await _repository.Create(entity);
            entity.Name = "Updated Name";

            // Act
            var result = await _repository.Update(entity);

            // Assert
            result.Name.Should().Be("Updated Name");
            (await _repository.Get(entity.Id)).Name.Should().Be("Updated Name");
        }

        [Fact]
        public async Task Exist_ShouldReturnTrueIfEntityExists()
        {
            // Arrange
            var entity = new TestEntity { Name = "Check Exist" };
            await _repository.Create(entity);

            // Act
            var exists = await _repository.Exist(entity.Id);

            // Assert
            exists.Should().BeTrue();
        }

        [Fact]
        public async Task Exist_ShouldReturnFalseIfEntityDoesNotExist()
        {
            // Act
            var exists = await _repository.Exist(999); // ID that doesn't exist

            // Assert
            exists.Should().BeFalse();
        }
    }

    public class TestEntity : Entity
    {
        public string Name { get; set; }
    }

    public class TestOMGDbContext : OMGDbContext
    {
        public TestOMGDbContext(DbContextOptions<TestOMGDbContext> options) : base(options) { }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
