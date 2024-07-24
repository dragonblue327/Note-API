using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Infrastructure.Data;
using Note.Infrastructure.Repository;

namespace TestNoteProjcet.RepositoryTests
{
    public class TagRepositoryTests : IDisposable
    {
        private readonly AppDBContext _context;
        private readonly TagRepository _repository;

        public TagRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDBContext(options);
            _repository = new TagRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        [Fact]
        public async Task CreateAsync_ShouldAddTag()
        {
            // Arrange
            var tag = new Tag { Name = "Test Tag" };

            // Act
            var result = await _repository.CreateAsync(tag);

            // Assert
            var createdTag = await _context.Tags.FindAsync(result.Id);
            Assert.NotNull(createdTag);
            Assert.Equal("Test Tag", createdTag.Name);
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteTag()
        {
            // Arrange
            var tag = new Tag { Name = "Test Tag" };
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(tag.Id);

            // Assert
            var deletedTag = await _context.Tags.FindAsync(tag.Id);
            Assert.Null(deletedTag);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllTagsAsync_ShouldReturnAllTags()
        {
            // Arrange
            var tags = new List<Tag>
                {
                    new Tag { Name = "Tag 1" },
                    new Tag { Name = "Tag 2" }
                };
            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllTagsAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Tag 1", result[0].Name);
            Assert.Equal("Tag 2", result[1].Name);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnTag()
        {
            // Arrange
            var tag = new Tag { Name = "Test Tag" };
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(tag.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Tag", result.Name);
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateTag()
        {
            // Arrange
            var tag = new Tag { Name = "Old Tag" };
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            var updatedTag = new Tag { Name = "Updated Tag", Notes = new List<Note.Domain.Entity.Note>(), Reminders = new List<Reminder>() };

            // Act
            var result = await _repository.UpdateAsync(tag.Id, updatedTag);

            // Assert
            var updatedEntity = await _context.Tags.FindAsync(tag.Id);
            Assert.Equal(1, result);
            Assert.Equal("Updated Tag", updatedEntity.Name);
        }

    }

}
