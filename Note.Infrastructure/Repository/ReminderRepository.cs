using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Infrastructure.Data;

namespace Note.Infrastructure.Repository
{
	public class ReminderRepository : IReminderRepository
	{
		private readonly AppDBContext _context;

		public ReminderRepository(AppDBContext appDBContext)
		{
			this._context = appDBContext;
		}
		public async Task<Reminder> CreateAsync(Reminder reminder)
		{
			try
			{
				await _context.Reminders.AddAsync(reminder);
				await _context.SaveChangesAsync();
				return reminder;
			}
			catch (DbUpdateException ex)
			{
				throw new Exception($"An error occurred while updating the database: {ex.Message}", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
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
				return await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}

		public async Task<List<Reminder>> GetAllNotesAsync()
		{
			try
			{
				return await _context.Reminders.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}

		public async Task<Reminder> GetByIdAsync(int id)
		{
			try
			{
				return await _context.Reminders.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}

		public async Task<int> UpdateAsync(int id, Reminder reminder)
		{
			try
			{
				var tempReminder = await _context.Reminders.Include(n => n.Tags).FirstOrDefaultAsync(a => a.Id == id);
				if (tempReminder == null)
				{
					return 0;
				}

				tempReminder.Title = reminder.Title;
				tempReminder.Text = reminder.Text;
				tempReminder.ReminderTime = reminder.ReminderTime;

				tempReminder.Tags.Clear();
				if (reminder.Tags != null)
				{
					foreach (var tag in reminder.Tags)
					{
						var existingTag = await _context.Tags.FindAsync(tag.Id);
						if (existingTag != null)
						{
							tempReminder.Tags.Add(existingTag);
						}
						else
						{
							var newTag = new Tag
							{
								Name = tag.Name
							};
							tempReminder.Tags.Add(newTag);
						}
					}
				}

				return await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}

	}
}
