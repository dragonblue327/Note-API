using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetReminder;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.RemindersCommandsTests
{
	public class GetReminderQueryHandlerTests
	{
		private readonly Mock<IReminderRepository> _mockReminderRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetReminderQueryHandler _handler;

		public GetReminderQueryHandlerTests()
		{
			_mockReminderRepository = new Mock<IReminderRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetReminderQueryHandler(_mockReminderRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnListOfReminderVm_WhenRemindersExist()
		{
			// Arrange
			var query = new GetReminderQuery();

			var reminderEntities = new List<Reminder>
			{
				new Reminder { Id = 1, Title = "Test Reminder 1", Text = "Test Text 1", ReminderTime = DateTime.Now, Tags = new List<Tag> { new Tag { Id = 1, Name = "Tag1" } } },
				new Reminder { Id = 2, Title = "Test Reminder 2", Text = "Test Text 2", ReminderTime = DateTime.Now, Tags = new List<Tag> { new Tag { Id = 2, Name = "Tag2" } } }
			};

			var reminderVms = new List<ReminderVm>
			{
				new ReminderVm { Id = 1, Title = "Test Reminder 1", Text = "Test Text 1", ReminderTime = DateTime.Now, Tags = new List<Tag> { new Tag { Id = 1, Name = "Tag1" } } },
				new ReminderVm { Id = 2, Title = "Test Reminder 2", Text = "Test Text 2", ReminderTime = DateTime.Now, Tags = new List<Tag> { new Tag { Id = 2, Name = "Tag2" } } }
			};

			_mockReminderRepository.Setup(repo => repo.GetAllRemindersAsync())
								   .ReturnsAsync(reminderEntities);

			_mockMapper.Setup(mapper => mapper.Map<List<ReminderVm>>(It.IsAny<List<Reminder>>()))
					   .Returns(reminderVms);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(reminderVms.Count, result.Count);
			Assert.Equal(reminderVms[0].Id, result[0].Id);
			Assert.Equal(reminderVms[1].Id, result[1].Id);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var query = new GetReminderQuery();

			_mockReminderRepository.Setup(repo => repo.GetAllRemindersAsync())
								   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
