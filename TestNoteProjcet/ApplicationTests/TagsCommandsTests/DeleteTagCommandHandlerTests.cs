using Moq;
using Note.Application.Notes.Commands.DeleteTag;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.TagsCommandsTests
{
	public class DeleteTagCommandHandlerTests
	{
		private readonly Mock<ITagRepository> _mockTagRepository;
		private readonly DeleteTagCommandHandler _handler;

		public DeleteTagCommandHandlerTests()
		{
			_mockTagRepository = new Mock<ITagRepository>();
			_handler = new DeleteTagCommandHandler(_mockTagRepository.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnTagId_WhenTagIsDeleted()
		{
			// Arrange
			var command = new DeleteTagCommand { Id = 1 };
			_mockTagRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
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
			var command = new DeleteTagCommand { Id = 1 };
			_mockTagRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
							  .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
