using Microsoft.EntityFrameworkCore;
using Note.Application.Notes.Queries.GetReminders;
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
		public async Task<Tag> CreateAsync(Tag tag)
		{
			try
			{
				foreach (var reminder in tag.Reminders)
				{
					reminder.Id = 0;
				}
				foreach (var note in tag.Notes)
				{
					note.Id = 0;
				}
				await _context.Tags.AddAsync(tag);
				await _context.SaveChangesAsync();
				return tag;
			}
			catch (DbUpdateException ex)
			{
				throw new Exception($"An error occurred while updating the database CreateAsync: {ex.Message}", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while creating the tag: {ex.Message}", ex);
			}
		}

		public async Task<int> DeleteAsync(int id)
		{
			try
			{
				var tag = await _context.Tags.FindAsync(id);
				if (tag == null)
				{
					return 0;
				}

				_context.Tags.Remove(tag);
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database DeleteAsync.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the tag: {ex.Message}", ex);
			}
		}

		public async Task<List<Tag>> GetAllTagsAsync()
		{
			try
			{

				return await _context.Tags.ToListAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database GetAllTagsAsync.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while get all tags: {ex.Message}", ex);
			}
		}

		public async Task<Tag> GetByIdAsync(int id)
		{
			try
			{
				return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database GetByIdAsync.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while get tag by id: {ex.Message}", ex);
			}
		}

		public async Task<int> UpdateAsync(int id, Tag tag)
		{
			try
			{
				var existingTag = await _context.Tags.Include(t => t.Notes).Include(t => t.Reminders).FirstOrDefaultAsync(t => t.Id == id);
				if (existingTag == null)
				{
					return 0;
				}
				existingTag.Name = tag.Name;
				if (existingTag.Notes != null)
				{
					existingTag.Notes.Clear();
				}
				if (tag.Notes != null)
				{
					foreach (var note in tag.Notes)
					{
						var existingNote = await _context.Notes.FindAsync(note.Id);
						if (existingNote != null)
						{
							if (existingNote.Title != note.Title)
							{
								existingNote.Title = note.Title ?? existingNote.Title;
							}
							if (existingNote.Text != note.Text)
							{
								existingNote.Text = note.Text ?? existingNote.Text;
							}
							if (existingNote.Tags == null)
							{
								existingNote.Tags = new List<Tag>();
							}
							existingNote.Tags.Add(existingTag);
							if (existingTag.Notes == null)
							{
								existingTag.Notes = new List<Domain.Entity.Note>();
							}
							existingTag.Notes.Add(existingNote);
						}
						else
						{
							var newNote = new Domain.Entity.Note
							{
								Text = note.Text,
								Title = note.Title
							};
							if (newNote.Tags == null)
							{
								newNote.Tags = new List<Tag>();
							}
							newNote.Tags.Add(existingTag);
							existingTag.Notes.Add(newNote);
						}
					}
				}
				existingTag.Reminders.Clear();
				if (tag.Reminders != null)
				{
					foreach (var reminder in tag.Reminders)
					{
						var existingReminder = await _context.Reminders.FindAsync(reminder.Id);
						if (existingReminder != null)
						{
							if (existingReminder.Title != reminder.Title)
							{
								existingReminder.Title = reminder.Title ?? existingReminder.Title;
							}
							if (existingReminder.Text != reminder.Text)
							{
								existingReminder.Text = reminder.Text ?? existingReminder.Text;
							}
							if (existingReminder.Tags == null)
							{
								existingReminder.Tags = new List<Tag>();
							}
							existingReminder.Tags.Add(existingTag);
							existingTag.Reminders.Add(existingReminder);
						}
						else
						{
							var newReminder = new Reminder
							{
								Text = reminder.Text,
								Title = reminder.Title,
								ReminderTime = reminder.ReminderTime
							};
							if (newReminder.Tags == null)
							{
								newReminder.Tags = new List<Tag>();
							}
							newReminder.Tags.Add(existingTag);
							existingTag.Reminders.Add(newReminder);
						}
					}
				}
				_context.Update(existingTag);
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the tag: {ex.Message}", ex);
			}
		}


	}
}
