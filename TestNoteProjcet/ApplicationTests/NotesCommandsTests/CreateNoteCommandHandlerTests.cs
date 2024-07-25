using AutoMapper;
using Note.Application.Notes.Commands.CreateNote;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Moq;

namespace TestNoteProjcet.ApplicationTests.NotesCommandsTests
{
	public class CreateReminderCommandHandlerTests
	{
		private readonly Mock<INoteRepository> _mockNoteRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly CreateReminderCommandHandler _handler;

		public CreateReminderCommandHandlerTests()
		{
			_mockNoteRepository = new Mock<INoteRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new CreateReminderCommandHandler(_mockNoteRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnNoteVm_WhenNoteIsCreated()
		{
			// Arrange
			var command = new CreateNoteCommand
			{
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			var noteEntity = new Note.Domain.Entity.Note
			{
				Id = 1,
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			var noteVm = new NoteVm
			{
				Id = 1,
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			_mockNoteRepository.Setup(repo => repo.CreateAsync(It.IsAny<Note.Domain.Entity.Note>()))
							   .ReturnsAsync(noteEntity);

			_mockMapper.Setup(mapper => mapper.Map<NoteVm>(It.IsAny<Note.Domain.Entity.Note>()))
					   .Returns(noteVm);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(noteVm.Id, result.Id);
			Assert.Equal(noteVm.Title, result.Title);
			Assert.Equal(noteVm.Text, result.Text);
			Assert.Equal(noteVm.Tags.Count, result.Tags.Count);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new CreateNoteCommand
			{
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			_mockNoteRepository.Setup(repo => repo.CreateAsync(It.IsAny<Note.Domain.Entity.Note>()))
							   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}


