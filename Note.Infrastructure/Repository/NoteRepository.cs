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

			await _context.Notes.AddAsync(note);
			await _context.SaveChangesAsync();
			
			return note;
			
		}

		public async Task<int> DeleteAsync(int id)
		{
			var note = await _context.Notes.FindAsync(id);
			if (note == null)
			{
				return 0; // Note not found
			}

			_context.Notes.Remove(note);
			return await _context.SaveChangesAsync();
		}

		public async Task<List<Domain.Entity.Note>> GetAllNotesAsync()
		{
			return await _context.Notes.ToListAsync();
		}

		public async Task<Domain.Entity.Note> GetByIdAsync(int id)
		{
			return await _context.Notes.AsNoTracking().Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<int> UpdateAsync(int id, Domain.Entity.Note note)
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
	}
}
