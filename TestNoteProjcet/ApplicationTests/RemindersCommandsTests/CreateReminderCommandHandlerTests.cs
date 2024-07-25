using AutoMapper;
using Moq;
using Note.Application.Notes.Commands.CreateReminder;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.RemindersCommandsTests
{
	public class CreateReminderCommandHandlerTests
	{
		private readonly Mock<IReminderRepository> _mockReminderRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly CreateReminderCommandHandler _handler;

		public CreateReminderCommandHandlerTests()
		{
			_mockReminderRepository = new Mock<IReminderRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new CreateReminderCommandHandler(_mockReminderRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnReminderVm_WhenReminderIsCreated()
		{
			// Arrange
			var command = new CreateReminderCommand
			{
				Title = "Test Reminder",
				Text = "Test Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			var reminderEntity = new Reminder
			{
				Id = 1,
				Title = "Test Reminder",
				Text = "Test Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			var reminderVm = new ReminderVm
			{
				Id = 1,
				Title = "Test Reminder",
				Text = "Test Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			_mockReminderRepository.Setup(repo => repo.CreateAsync(It.IsAny<Reminder>()))
								   .ReturnsAsync(reminderEntity);

			_mockMapper.Setup(mapper => mapper.Map<ReminderVm>(It.IsAny<Reminder>()))
					   .Returns(reminderVm);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(reminderVm.Id, result.Id);
			Assert.Equal(reminderVm.Title, result.Title);
			Assert.Equal(reminderVm.Text, result.Text);
			Assert.Equal(reminderVm.ReminderTime, result.ReminderTime);
			Assert.Equal(reminderVm.Tags.Count, result.Tags.Count);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new CreateReminderCommand
			{
				Title = "Test Reminder",
				Text = "Test Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			_mockReminderRepository.Setup(repo => repo.CreateAsync(It.IsAny<Reminder>()))
								   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
