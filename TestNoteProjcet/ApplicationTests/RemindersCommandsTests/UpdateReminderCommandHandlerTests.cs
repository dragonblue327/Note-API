using AutoMapper;
using Moq;
using Note.Application.Notes.Commands.UpdateReminder;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;
namespace TestNoteProjcet.ApplicationTests.RemindersCommandsTests
{
	public class UpdateReminderCommandHandlerTests
	{
		private readonly Mock<IReminderRepository> _mockReminderRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly UpdateReminderCommandHandler _handler;

		public UpdateReminderCommandHandlerTests()
		{
			_mockReminderRepository = new Mock<IReminderRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new UpdateReminderCommandHandler(_mockReminderRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnReminderVm_WhenReminderIsUpdated()
		{
			// Arrange
			var command = new UpdateReminderCommand
			{
				Id = 1,
				Title = "Updated Reminder",
				Text = "Updated Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			var reminderEntity = new Reminder
			{
				Id = 1,
				Title = "Updated Reminder",
				Text = "Updated Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			var reminderVm = new ReminderVm
			{
				Id = 1,
				Title = "Updated Reminder",
				Text = "Updated Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			_mockReminderRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Reminder>()))
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
			var command = new UpdateReminderCommand
			{
				Id = 1,
				Title = "Updated Reminder",
				Text = "Updated Text",
				ReminderTime = DateTime.Now,
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			_mockReminderRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Reminder>()))
								   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
