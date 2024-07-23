using Microsoft.EntityFrameworkCore;
using Note.Domain.Repository;
using Note.Interface.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Interface.Repository
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
			return await _context.Notes.Where(a => a.Id == id).ExecuteDeleteAsync();
		}

		public async Task<List<Domain.Entity.Note>> GetAllNotesAsync()
		{
			return await _context.Notes.ToListAsync();

		}

		public async Task<Domain.Entity.Note> GetByIdAsync(int id)
		{
			return await _context.Notes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<int> UpdateAsync(int id, Domain.Entity.Note note)
		{
			return await _context.Notes.Where(a => a.Id == id).ExecuteUpdateAsync(setters => setters
			.SetProperty(a => a.Title, note.Title)
			.SetProperty(a => a.Text, note.Text)
			);
		}
	}
}
