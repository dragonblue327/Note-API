using Moq;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.RepositoryTests;
public class TagRepositoryTests
{
	private readonly Mock<ITagRepository> _tagRepositoryMock;

	public TagRepositoryTests()
	{
		_tagRepositoryMock = new Mock<ITagRepository>();
	}

	[Fact]
	public async Task GetAllTagsAsync_ShouldReturnListOfTags()
	{
		// Arrange
		var tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } };
		_tagRepositoryMock.Setup(repo => repo.GetAllTagsAsync()).ReturnsAsync(tags);

		// Act
		var result = await _tagRepositoryMock.Object.GetAllTagsAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal("Test Tag", result[0].Name);
	}

	[Fact]
	public async Task GetByIdAsync_ShouldReturnTag()
	{
		// Arrange
		var tag = new Tag { Id = 1, Name = "Test Tag" };
		_tagRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(tag);

		// Act
		var result = await _tagRepositoryMock.Object.GetByIdAsync(1);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("Test Tag", result.Name);
	}

	[Fact]
	public async Task CreateAsync_ShouldReturnCreatedTag()
	{
		// Arrange
		var tag = new Tag { Id = 1, Name = "New Tag" };
		_tagRepositoryMock.Setup(repo => repo.CreateAsync(tag)).ReturnsAsync(tag);

		// Act
		var result = await _tagRepositoryMock.Object.CreateAsync(tag);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("New Tag", result.Name);
	}

	[Fact]
	public async Task UpdateAsync_ShouldReturnUpdatedTag()
	{
		// Arrange
		var tag = new Tag { Id = 1, Name = "Updated Tag" };
		_tagRepositoryMock.Setup(repo => repo.UpdateAsync(1, tag)).ReturnsAsync(tag);

		// Act
		var result = await _tagRepositoryMock.Object.UpdateAsync(1, tag);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("Updated Tag", result.Name);
	}

	[Fact]
	public async Task DeleteAsync_ShouldReturnDeletedTagId()
	{
		// Arrange
		_tagRepositoryMock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(1);

		// Act
		var result = await _tagRepositoryMock.Object.DeleteAsync(1);

		// Assert
		Assert.Equal(1, result);
	}
}
