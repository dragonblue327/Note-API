using Microsoft.EntityFrameworkCore;
using Note.Infrastructure.Data;
using Note.Infrastructure.Repository;
using Note.Domain.Entity;

namespace TestNoteProjcet
{
	public class NoteRepositoryTests
	{
		private readonly AppDBContext _context;
		private readonly NoteRepository _repository;

		public NoteRepositoryTests()
		{
			var options = new DbContextOptionsBuilder<AppDBContext>()
		   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
		   .Options;

			_context = new AppDBContext(options);
			_repository = new NoteRepository(_context);
		}

		// Dispose the context after each test
		public void Dispose()
		{
			_context.Dispose();
		}
		[Fact]
		public async Task CreateAsync_ShouldAddNote()
		{
			// Arrange
			var note = new Note.Domain.Entity.Note { Title = "Test Note", Text = "Test Text" };

			// Act
			var result = await _repository.CreateAsync(note);

			// Assert
			var createdNote = await _context.Notes.FindAsync(result.Id);
			Assert.NotNull(createdNote);
			Assert.Equal("Test Note", createdNote.Title);
			Assert.Equal("Test Text", createdNote.Text);
		}
		[Fact]
		public async Task DeleteAsync_ShouldDeleteNote()
		{
			// Arrange
			var note = new Note.Domain.Entity.Note { Title = "Test Note", Text = "Test Text" };
			await _context.Notes.AddAsync(note);
			await _context.SaveChangesAsync();

			// Act
			var result = await _repository.DeleteAsync(note.Id);

			// Assert
			var deletedNote = await _context.Notes.FindAsync(note.Id);
			Assert.Null(deletedNote);
			Assert.Equal(1, result);
		}
		[Fact]
		public async Task GetAllNotesAsync_ShouldReturnAllNotes()
		{
			// Arrange
			var notes = new List<Note.Domain.Entity.Note>
				{
					new Note.Domain.Entity.Note { Title = "Note 1", Text = "Text 1" },
					new Note.Domain.Entity.Note { Title = "Note 2", Text = "Text 2" }
				};
			await _context.Notes.AddRangeAsync(notes);
			await _context.SaveChangesAsync();

			// Act
			var result = await _repository.GetAllNotesAsync();

			// Assert
			Assert.Equal(2, result.Count);
			Assert.Equal("Note 1", result[0].Title);
			Assert.Equal("Note 2", result[1].Title);
		}

		[Fact]
		public async Task GetByIdAsync_ShouldReturnNote()
		{
			// Arrange
			var note = new Note.Domain.Entity.Note { Title = "Test Note", Text = "Test Text" };
			await _context.Notes.AddAsync(note);
			await _context.SaveChangesAsync();

			// Act
			var result = await _repository.GetByIdAsync(note.Id);

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Test Note", result.Title);
			Assert.Equal("Test Text", result.Text);
		}

		[Fact]
		public async Task UpdateAsync_ShouldUpdateNote()
		{
			// Arrange
			var note = new Note.Domain.Entity.Note { Title = "Old Note", Text = "Old Text" };
			await _context.Notes.AddAsync(note);
			await _context.SaveChangesAsync();

			var updatedNote = new Note.Domain.Entity.Note { Title = "Updated Note", Text = "Updated Text", Tags = new List<Tag>() };

			// Act
			var result = await _repository.UpdateAsync(note.Id, updatedNote);

			// Assert
			var updatedEntity = await _context.Notes.FindAsync(note.Id);
			Assert.Equal(1, result);
			Assert.Equal("Updated Note", updatedEntity.Title);
			Assert.Equal("Updated Text", updatedEntity.Text);
		}


	}
}