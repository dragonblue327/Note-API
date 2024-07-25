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
				foreach (var tag in reminder.Tags)
				{
					tag.Id = 0;
				}
				await _context.Reminders.AddAsync(reminder);
				await _context.SaveChangesAsync();
				return reminder;
			}
			catch (DbUpdateException ex)
			{
				throw new Exception($"An error occurred while updating the database CreateAsync : {ex.Message}", ex);
			}
			catch (Exception ex)
			{
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
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database DeleteAsync.", ex);
			}
			catch (Exception ex)
			{
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
				throw new Exception("An error occurred while updating the database GetAllReminderAsync.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while get all reminders: {ex.Message}", ex);
			}
		}

		public async Task<Reminder> GetByIdAsync(int id)
		{
			try
			{
				return await _context.Reminders.AsNoTracking().Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database GetByIdAsync.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while get reminder by id: {ex.Message}", ex);
			}
		}

		public async Task<int> UpdateAsync(int id, Reminder reminder)
		{
			try
			{
				var existingReminder = await _context.Reminders.Include(r => r.Tags).FirstOrDefaultAsync(r => r.Id == id);
				if (existingReminder == null)
				{
					return 0;
				}

				existingReminder.Title = reminder.Title;
				existingReminder.Text = reminder.Text;
				existingReminder.ReminderTime = reminder.ReminderTime;

				existingReminder.Tags.Clear();

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
							
							existingTag.Reminders.Add(existingReminder);
							existingReminder.Tags.Add(existingTag);
						}
						else
						{
							var newTag = new Tag
							{
								Name = tag.Name
							};
							newTag.Reminders.Add(existingReminder);
							existingReminder.Tags.Add(newTag);
						}
					}
				}

				_context.Update(existingReminder);
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the reminder: {ex.Message}", ex);
			}
		}


	}
}
