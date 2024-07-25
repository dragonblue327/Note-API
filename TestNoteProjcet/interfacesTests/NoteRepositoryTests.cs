
using Moq;
using Note.Domain.Repository;

namespace TestNoteProjcet.RepositoryTests;
public class NoteRepositoryTests
{
	private readonly Mock<INoteRepository> _noteRepositoryMock;

	public NoteRepositoryTests()
	{
		_noteRepositoryMock = new Mock<INoteRepository>();
	}
	[Fact]
	public async Task GetAllNotesAsync_ShouldReturnListOfNotes()
	{
		// Arrange
		var notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } };
		_noteRepositoryMock.Setup(repo => repo.GetAllNotesAsync()).ReturnsAsync(notes);

		// Act
		var result = await _noteRepositoryMock.Object.GetAllNotesAsync();

		// Assert
		Assert.NotNull(result);
		Assert.Single(result);
		Assert.Equal("Test Note", result[0].Title);
	}
	[Fact]
	public async Task GetByIdAsync_ShouldReturnNote()
	{
		// Arrange
		var note = new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" };
		_noteRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(note);

		// Act
		var result = await _noteRepositoryMock.Object.GetByIdAsync(1);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("Test Note", result.Title);
	}

	[Fact]
	public async Task CreateAsync_ShouldReturnCreatedNote()
	{
		// Arrange
		var note = new Note.Domain.Entity.Note { Id = 1, Title = "New Note" };
		_noteRepositoryMock.Setup(repo => repo.CreateAsync(note)).ReturnsAsync(note);

		// Act
		var result = await _noteRepositoryMock.Object.CreateAsync(note);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("New Note", result.Title);
	}

	[Fact]
	public async Task UpdateAsync_ShouldReturnUpdatedNote()
	{
		// Arrange
		var note = new Note.Domain.Entity.Note { Id = 1, Title = "Updated Note" };
		_noteRepositoryMock.Setup(repo => repo.UpdateAsync(1, note)).ReturnsAsync(note);

		// Act
		var result = await _noteRepositoryMock.Object.UpdateAsync(1, note);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(1, result.Id);
		Assert.Equal("Updated Note", result.Title);
	}

	[Fact]
	public async Task DeleteAsync_ShouldReturnDeletedNoteId()
	{
		// Arrange
		_noteRepositoryMock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(1);

		// Act
		var result = await _noteRepositoryMock.Object.DeleteAsync(1);

		// Assert
		Assert.Equal(1, result);
	}

}
