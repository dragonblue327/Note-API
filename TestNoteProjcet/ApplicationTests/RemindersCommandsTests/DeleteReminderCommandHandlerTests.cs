using Moq;
using Note.Application.Notes.Commands.DeleteReminder;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.RemindersCommandsTests
{
	public class DeleteReminderCommandHandlerTests
	{
		private readonly Mock<IReminderRepository> _mockReminderRepository;
		private readonly DeleteReminderCommandHandler _handler;

		public DeleteReminderCommandHandlerTests()
		{
			_mockReminderRepository = new Mock<IReminderRepository>();
			_handler = new DeleteReminderCommandHandler(_mockReminderRepository.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnReminderId_WhenReminderIsDeleted()
		{
			// Arrange
			var command = new DeleteReminderCommand { Id = 1 };
			_mockReminderRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
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
			var command = new DeleteReminderCommand { Id = 1 };
			_mockReminderRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
								   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
