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
				foreach (var tag in note.Tags!) {
					tag.Id = 0;
				}
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
				return await _context.Notes.Include(a=>a.Tags).ToListAsync();
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
				var tempNote=  await _context.Notes.AsNoTracking()
										   .Include(a => a.Tags)
										   .FirstOrDefaultAsync(a => a.Id == id);
				return tempNote ?? new Domain.Entity.Note();
				
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

		public async Task<Domain.Entity.Note> UpdateAsync(int id, Domain.Entity.Note note)
		{
			try
			{
				var existingNote = await _context.Notes.Include(n => n.Tags).FirstOrDefaultAsync(a => a.Id == id);
				if (existingNote == null)
				{
					return new Domain.Entity.Note();
				}

				existingNote.Title = note.Title;
				existingNote.Text = note.Text;

				existingNote.Tags?.Clear();

				if (note.Tags != null)
				{
					foreach (var tag in note.Tags)
					{
						var existingTag = await _context.Tags.FindAsync(tag.Id);
						if (existingTag != null)
						{
							if (existingTag.Name != tag.Name)
							{
								existingTag.Name = tag.Name ?? existingTag.Name;
							}
							existingNote.Tags!.Add(existingTag);
						}
						else
						{
							var newTag = new Tag
							{
								Name = tag.Name!
							};
							newTag.Notes!.Add(existingNote);
							existingNote.Tags!.Add(newTag);
						}
					}
				}

				_context.Update(existingNote);
				await _context.SaveChangesAsync();
				return existingNote;
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
