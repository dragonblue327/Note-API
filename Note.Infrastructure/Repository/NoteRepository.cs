using Microsoft.EntityFrameworkCore;
using Note.Domain.Entity;
using Note.Domain.Repository;
using Note.Infrastructure.Data;
using Logs.Info;

namespace Note.Infrastructure.Repository
{
	public class NoteRepository : INoteRepository
	{
		private static MemoryLogger _logger =  MemoryLogger.GetLogger;
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
				_logger.LogInfo($"Item type note with id : ( {note.Id} ) created");
				return note;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
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
				_logger.LogInfo($"Item type note with id : ( {id} ) deleted");
				return await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
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
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while retrieving the notes from the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
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
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while retrieving the note from the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
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
				_logger.LogInfo($"Item type note with id : ( {id} ) changed");
				_context.Update(existingNote);
				await _context.SaveChangesAsync();
				return existingNote;
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex.Message);
				throw new Exception("An error occurred while updating the database.", ex);
			}
			catch (Exception ex)
			{
				_logger.LogWarning(ex.Message);
				throw new Exception("An error occurred while updating the note.", ex);
			}
		}



	}
}
