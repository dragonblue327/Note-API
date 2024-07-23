using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Note.Domain.Entity;
namespace Note.Domain.Repository
{
	public interface IReminderRepository
	{
		Task<List<Reminder>> GetAllNotesAsync();
		Task<Reminder> GetByIdAsync(int id);
		Task<Reminder> CreateAsync(Reminder reminder);
		Task<int> UpdateAsync(int id, Reminder reminder);
		Task<int> DeleteAsync(int id);

	}
}
