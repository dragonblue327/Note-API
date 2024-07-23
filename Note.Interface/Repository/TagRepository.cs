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
			return await _context.Tags.Where(a => a.Id == id).ExecuteDeleteAsync();
		}

		public async Task<List<Tag>> GetAllTagsAsync()
		{
			return await _context.Tags.ToListAsync();

		}

		public async Task<Tag> GetByIdAsync(int id)
		{
			return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<int> UpdateAsync(int id, Domain.Entity.Tag Tag)
		{
			return await _context.Tags.Where(a => a.Id == id).ExecuteUpdateAsync(setters => setters
			.SetProperty(a => a.Name, Tag.Name)
			);
		}
	}
}
