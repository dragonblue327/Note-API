using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Domain.Entity;
namespace Note.Domain.Repository
{
	public interface INoteRepository 
	{
		Task<List<Entity.Note>> GetAllNotesAsync();
		Task<Entity.Note> GetByIdAsync(int id);
		Task<Entity.Note> CreateAsync(Entity.Note note);
		Task<Domain.Entity.Note> UpdateAsync(int id, Entity.Note note);
		Task<int> DeleteAsync(int id);

	}
}
