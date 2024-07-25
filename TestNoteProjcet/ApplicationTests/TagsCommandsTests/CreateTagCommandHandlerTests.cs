using AutoMapper;
using Moq;
using Note.Application.Notes.Commands.CreateTag;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.TagsCommandsTests
{
	public class CreateTagCommandHandlerTests
	{
		private readonly Mock<ITagRepository> _mockTagRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly CreateTagCommandHandler _handler;

		public CreateTagCommandHandlerTests()
		{
			_mockTagRepository = new Mock<ITagRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new CreateTagCommandHandler(_mockTagRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnTagVm_WhenTagIsCreated()
		{
			// Arrange
			var command = new CreateTagCommand
			{
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			var tagEntity = new Tag
			{
				Id = 1,
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			var tagVm = new TagVm
			{
				Id = 1,
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			_mockTagRepository.Setup(repo => repo.CreateAsync(It.IsAny<Tag>()))
							  .ReturnsAsync(tagEntity);

			_mockMapper.Setup(mapper => mapper.Map<TagVm>(It.IsAny<Tag>()))
					   .Returns(tagVm);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(tagVm.Id, result.Id);
			Assert.Equal(tagVm.Name, result.Name);
			Assert.Equal(tagVm.Notes.Count, result.Notes.Count);
			Assert.Equal(tagVm.Reminders.Count, result.Reminders.Count);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new CreateTagCommand
			{
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			_mockTagRepository.Setup(repo => repo.CreateAsync(It.IsAny<Tag>()))
							  .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
