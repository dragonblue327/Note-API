using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Infrastructure.Data;

namespace Note.Infrastructure.Repository
{
	public class NoteRepository : INoteRepository
	{
		private readonly AppDBContext _context;

		public NoteRepository(AppDBContext appDBContext)
		{
			this._context = appDBContext;
		}
		public async Task<Domain.Entity.Note> CreateAsync(Domain.Entity.Note note)
		{
			try
			{
				await _context.Notes.AddAsync(note);
				await _context.SaveChangesAsync();
				return note;
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while creating the note.", ex);
			}

		}

		public async Task<int> DeleteAsync(int id)
		{
			try
			{
				var note = await _context.Notes.FindAsync(id);
				if (note == null)
				{
					return 0;
				}

				_context.Notes.Remove(note);
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while deleting the note.", ex);
			}

		}

		public async Task<List<Domain.Entity.Note>> GetAllNotesAsync()
		{
			try
			{
				return await _context.Notes.ToListAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while retrieving the notes from the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while retrieving the notes.", ex);
			}

		}

		public async Task<Domain.Entity.Note> GetByIdAsync(int id)
		{
			try
			{
				return await _context.Notes.AsNoTracking()
										   .Include(a => a.Tags)
										   .FirstOrDefaultAsync(a => a.Id == id);
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while retrieving the note from the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while retrieving the note.", ex);
			}

		}

		public async Task<int> UpdateAsync(int id, Domain.Entity.Note note)
		{
			try
			{
				var tempNote = await _context.Notes.Include(n => n.Tags).FirstOrDefaultAsync(a => a.Id == id);
				if (tempNote == null)
				{
					return 0;
				}

				tempNote.Title = note.Title;
				tempNote.Text = note.Text;

				tempNote.Tags.Clear();
				if (note.Tags != null)
				{
					foreach (var tag in note.Tags)
					{
						var existingTag = await _context.Tags.FindAsync(tag.Id);
						if (existingTag != null)
						{
							tempNote.Tags.Add(existingTag);
						}
						else
						{
							var newTag = new Tag
							{
								Name = tag.Name
							};
							tempNote.Tags.Add(newTag);
						}
					}
				}

				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while updating the note.", ex);
			}

		}
	}
}
