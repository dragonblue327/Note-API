using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Infrastructure.Data;

namespace Note.Infrastructure.Repository
{
	public class TagRepository : ITagRepository
	{
		private readonly AppDBContext _context;

		public TagRepository(AppDBContext appDBContext)
		{
			this._context = appDBContext;
		}
		public async Task<Tag> CreateAsync(Tag Tag)
		{
			await _context.Tags.AddAsync(Tag);
			await _context.SaveChangesAsync();
			return Tag;
		}

		public async Task<int> DeleteAsync(int id)
		{
			var tag = await _context.Tags.FindAsync(id);
			if (tag == null)
			{
				return 0; // Tag not found
			}

			_context.Tags.Remove(tag);
			return await _context.SaveChangesAsync();
		}


		public async Task<List<Tag>> GetAllTagsAsync()
		{
			return await _context.Tags.ToListAsync();

		}

		public async Task<Tag> GetByIdAsync(int id)
		{
			return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<int> UpdateAsync(int id, Tag tag)
		{
			var tempTag = await _context.Tags.Include(n => n.Notes).Include(n => n.Reminders).FirstOrDefaultAsync(a => a.Id == id);
			if (tempTag == null)
			{
				return 0;
			}
			tempTag.Name = tag.Name;
			tempTag.Notes.Clear();
			if (tag.Notes != null)
			{
				foreach (var note in tag.Notes)
				{
					var existingNote = await _context.Notes.FindAsync(note.Id);
					if (existingNote != null)
					{
						tempTag.Notes.Add(existingNote);
					}
					else
					{
						var newNote = new Domain.Entity.Note()
						{
							Text = note.Text,
							Title = note.Title
						};
						tempTag.Notes.Add(newNote);
					}
				}
			}
			if (tag.Reminders != null)
			{
				foreach (var reminder in tag.Reminders)
				{
					var existingReminder = await _context.Reminders.FindAsync(reminder.Id);
					if (existingReminder != null)
					{
						tempTag.Reminders.Add(existingReminder);
					}
					else
					{
						var newReminder = new Reminder
						{
							Text = reminder.Text,
							Title = reminder.Title,
							ReminderTime = reminder.ReminderTime,

						};
						tempTag.Reminders.Add(newReminder);
					}
				}
			}
			return await _context.SaveChangesAsync();
		}
	}
}
