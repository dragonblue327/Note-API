using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Note.API.Entity
{
	public class TagDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ICollection<NoteDto>? Notes { get; set; }
		public ICollection<ReminderDto>? Reminders { get; set; }
	}
}
