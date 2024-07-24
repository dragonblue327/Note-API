using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Infrastructure.Data;
using Note.Infrastructure.Repository;

namespace TestNoteProjcet.RepositoryTests
{
    public class ReminderRepositoryTests : IDisposable
    {
        private readonly AppDBContext _context;
        private readonly ReminderRepository _repository;

        public ReminderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDBContext(options);
            _repository = new ReminderRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        [Fact]
        public async Task CreateAsync_ShouldAddReminder()
        {
            // Arrange
            var reminder = new Reminder { Title = "Test Reminder", Text = "Test Text", ReminderTime = DateTime.Now };

            // Act
            var result = await _repository.CreateAsync(reminder);

            // Assert
            var createdReminder = await _context.Reminders.FindAsync(result.Id);
            Assert.NotNull(createdReminder);
            Assert.Equal("Test Reminder", createdReminder.Title);
            Assert.Equal("Test Text", createdReminder.Text);
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteReminder()
        {
            // Arrange
            var reminder = new Reminder { Title = "Test Reminder", Text = "Test Text", ReminderTime = DateTime.Now };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteAsync(reminder.Id);

            // Assert
            var deletedReminder = await _context.Reminders.FindAsync(reminder.Id);
            Assert.Null(deletedReminder);
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldReturnAllReminders()
        {
            // Arrange
            var reminders = new List<Reminder>
                {
                    new Reminder { Title = "Reminder 1", Text = "Text 1", ReminderTime = DateTime.Now },
                    new Reminder { Title = "Reminder 2", Text = "Text 2", ReminderTime = DateTime.Now }
                };
            await _context.Reminders.AddRangeAsync(reminders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllNotesAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Reminder 1", result[0].Title);
            Assert.Equal("Reminder 2", result[1].Title);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnReminder()
        {
            // Arrange
            var reminder = new Reminder { Title = "Test Reminder", Text = "Test Text", ReminderTime = DateTime.Now };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync(reminder.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Reminder", result.Title);
            Assert.Equal("Test Text", result.Text);
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateReminder()
        {
            // Arrange
            var reminder = new Reminder { Title = "Old Reminder", Text = "Old Text", ReminderTime = DateTime.Now };
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();

            var updatedReminder = new Reminder { Title = "Updated Reminder", Text = "Updated Text", ReminderTime = DateTime.Now.AddHours(1), Tags = new List<Tag>() };

            // Act
            var result = await _repository.UpdateAsync(reminder.Id, updatedReminder);

            // Assert
            var updatedEntity = await _context.Reminders.FindAsync(reminder.Id);
            Assert.Equal(1, result);
            Assert.Equal("Updated Reminder", updatedEntity.Title);
            Assert.Equal("Updated Text", updatedEntity.Text);
            Assert.Equal(updatedReminder.ReminderTime, updatedEntity.ReminderTime);
        }

    }

}
