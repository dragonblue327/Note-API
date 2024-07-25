using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetReminderById;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.RemindersCommandsTests
{
	public class GetReminderByIdQueryHandlerTests
	{
		private readonly Mock<IReminderRepository> _mockReminderRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetReminderByIdQueryHandler _handler;

		public GetReminderByIdQueryHandlerTests()
		{
			_mockReminderRepository = new Mock<IReminderRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetReminderByIdQueryHandler(_mockReminderRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnReminderVm_WhenReminderExists()
		{
			// Arrange
			var query = new GetReminderByIdQuery { ReminderId = 1 };

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

			_mockReminderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
								   .ReturnsAsync(reminderEntity);

			_mockMapper.Setup(mapper => mapper.Map<ReminderVm>(It.IsAny<Reminder>()))
					   .Returns(reminderVm);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

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
			var query = new GetReminderByIdQuery { ReminderId = 1 };

			_mockReminderRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
								   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
