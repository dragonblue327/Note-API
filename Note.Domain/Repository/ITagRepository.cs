using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Domain.Entity;
namespace Note.Domain.Repository
{
	public interface ITagRepository
	{
		Task<List<Tag>> GetAllNotesAsync();
		Task<Tag> GetByIdAsync(int id);
		Task<Tag> CreateAsync(Tag tag);
		Task<int> UpdateAsync(int id, Tag tag);
		Task<int> DeleteAsync(int id);

	}
}
