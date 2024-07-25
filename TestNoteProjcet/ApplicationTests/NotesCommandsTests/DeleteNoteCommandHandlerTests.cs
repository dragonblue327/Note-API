using Moq;
using Note.Application.Notes.Commands.DeleteNote;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.NotesCommandsTests
{
	public class DeleteNoteCommandHandlerTests
	{

		private readonly Mock<INoteRepository> _mockNoteRepository;
		private readonly DeleteNoteCommandHandler _handler;

		public DeleteNoteCommandHandlerTests()
		{
			_mockNoteRepository = new Mock<INoteRepository>();
			_handler = new DeleteNoteCommandHandler(_mockNoteRepository.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnNoteId_WhenNoteIsDeleted()
		{
			// Arrange
			var command = new DeleteNoteCommand { Id = 1 };
			_mockNoteRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
							   .ReturnsAsync(command.Id);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.Equal(command.Id, result);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new DeleteNoteCommand { Id = 1 };
			_mockNoteRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
							   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}

