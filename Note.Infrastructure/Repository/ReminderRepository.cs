using Logs.Info;
using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Infrastructure.Data;

namespace Note.Infrastructure.Repository
{
	public class ReminderRepository : IReminderRepository
	{
		private readonly AppDBContext _context;
		private static MemoryLogger _logger = MemoryLogger.GetLogger;
		public ReminderRepository(AppDBContext appDBContext)
		{
			this._context = appDBContext;
		}
		public async Task<Reminder> CreateAsync(Reminder reminder)
		{
			try
			{
				foreach (var tag in reminder.Tags!)
				{
					tag.Id = 0;
				}
				await _context.Reminders.AddAsync(reminder);
				await _context.SaveChangesAsync();

				_logger.LogInfo($"Item type reminder with id : ( {reminder.Id} ) created");
				return reminder;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception($"An error occurred while updating the database CreateAsync : {ex.Message}", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception($"An error occurred while creating the Reminder: {ex.Message}", ex);
			}
		}

		public async Task<int> DeleteAsync(int id)
		{
			try
			{
				var reminder = await _context.Reminders.FindAsync(id);
				if (reminder == null)
				{
					return 0;
				}

				_context.Reminders.Remove(reminder);
				_logger.LogInfo($"Item type reminder with id : ( {id} ) deleted");
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database DeleteAsync.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception($"An error occurred while delete the Reminder: {ex.Message}", ex);
			}
		}

		public async Task<List<Reminder>> GetAllRemindersAsync()
		{
			try
			{
				return await _context.Reminders.Include(a => a.Tags).ToListAsync();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database GetAllReminderAsync.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception($"An error occurred while get all reminders: {ex.Message}", ex);
			}
		}

		public async Task<Reminder> GetByIdAsync(int id)
		{
			try
			{
				var tempReminder = await _context.Reminders.AsNoTracking().Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);
				return tempReminder ?? new Reminder();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database GetByIdAsync.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception($"An error occurred while get reminder by id: {ex.Message}", ex);
			}
		}

		public async Task<Reminder> UpdateAsync(int id, Reminder reminder)
		{
			try
			{
				var existingReminder = await _context.Reminders.Include(r => r.Tags).FirstOrDefaultAsync(r => r.Id == id);
				if (existingReminder == null)
				{
					return new Reminder();
				}

				existingReminder.Title = reminder.Title;
				existingReminder.Text = reminder.Text;
				existingReminder.ReminderTime = reminder.ReminderTime;

				existingReminder.Tags!.Clear();

				if (reminder.Tags != null)
				{
					foreach (var tag in reminder.Tags)
					{
						var existingTag = await _context.Tags.FindAsync(tag.Id);
						if (existingTag != null)
						{
							if (existingTag.Name != tag.Name)
							{
								existingTag.Name = tag.Name ?? existingTag.Name;
							}

							existingTag.Reminders!.Add(existingReminder);
							existingReminder.Tags.Add(existingTag);
						}
						else
						{
							var newTag = new Tag
							{
								Name = tag.Name!
							};
							newTag.Reminders!.Add(existingReminder);
							existingReminder.Tags.Add(newTag);
						}
					}
				}
				_logger.LogInfo($"Item type reminder with id : ( {id} ) changed");
				_context.Update(existingReminder);
				await _context.SaveChangesAsync();
				return existingReminder;

			}
			catch (DbUpdateException ex)
			{

				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception($"An error occurred while updating the reminder: {ex.Message}", ex);
			}
		}


	}
}
