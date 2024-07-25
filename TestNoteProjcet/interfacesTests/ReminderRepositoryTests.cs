using Moq;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Xunit;

namespace TestNoteProjcet.RepositoryTests
{
	

	public class ReminderRepositoryTests
	{
		private readonly Mock<IReminderRepository> _reminderRepositoryMock;

		public ReminderRepositoryTests()
		{
			_reminderRepositoryMock = new Mock<IReminderRepository>();
		}

		[Fact]
		public async Task GetAllRemindersAsync_ShouldReturnListOfReminders()
		{
			// Arrange
			var reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } };
			_reminderRepositoryMock.Setup(repo => repo.GetAllRemindersAsync()).ReturnsAsync(reminders);

			// Act
			var result = await _reminderRepositoryMock.Object.GetAllRemindersAsync();

			// Assert
			Assert.NotNull(result);
			Assert.Single(result);
			Assert.Equal("Test Reminder", result[0].Title);
		}

		[Fact]
		public async Task GetByIdAsync_ShouldReturnReminder()
		{
			// Arrange
			var reminder = new Reminder { Id = 1, Title = "Test Reminder" };
			_reminderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(reminder);

			// Act
			var result = await _reminderRepositoryMock.Object.GetByIdAsync(1);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
			Assert.Equal("Test Reminder", result.Title);
		}

		[Fact]
		public async Task CreateAsync_ShouldReturnCreatedReminder()
		{
			// Arrange
			var reminder = new Reminder { Id = 1, Title = "New Reminder" };
			_reminderRepositoryMock.Setup(repo => repo.CreateAsync(reminder)).ReturnsAsync(reminder);

			// Act
			var result = await _reminderRepositoryMock.Object.CreateAsync(reminder);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
			Assert.Equal("New Reminder", result.Title);
		}

		[Fact]
		public async Task UpdateAsync_ShouldReturnUpdatedReminder()
		{
			// Arrange
			var reminder = new Reminder { Id = 1, Title = "Updated Reminder" };
			_reminderRepositoryMock.Setup(repo => repo.UpdateAsync(1, reminder)).ReturnsAsync(reminder);

			// Act
			var result = await _reminderRepositoryMock.Object.UpdateAsync(1, reminder);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(1, result.Id);
			Assert.Equal("Updated Reminder", result.Title);
		}

		[Fact]
		public async Task DeleteAsync_ShouldReturnDeletedReminderId()
		{
			// Arrange
			_reminderRepositoryMock.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(1);

			// Act
			var result = await _reminderRepositoryMock.Object.DeleteAsync(1);

			// Assert
			Assert.Equal(1, result);
		}
	}


}
