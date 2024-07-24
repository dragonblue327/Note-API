using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface.Repository
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
			await _context.Reminders.AddAsync(reminder);
			await _context.SaveChangesAsync();
			return reminder;
		}

		public async Task<int> DeleteAsync(int id)
		{
			return await _context.Reminders.Where(a => a.Id == id).ExecuteDeleteAsync();
		}

		public async Task<List<Reminder>> GetAllNotesAsync()
		{
			return await _context.Reminders.ToListAsync();

		}

		public async Task<Reminder> GetByIdAsync(int id)
		{
			return await _context.Reminders.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<int> UpdateAsync(int id, Reminder reminder)
		{
			return await _context.Reminders.Where(a => a.Id == id).ExecuteUpdateAsync(setters => setters
			.SetProperty(a => a.Title, reminder.Title)
			.SetProperty(a => a.Text, reminder.Text)
			.SetProperty(a => a.ReminderTime, reminder.ReminderTime)
			);
		}
	}
}
