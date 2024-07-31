using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Note.API.Entity
{
	public class ReminderDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public DateTime ReminderTime { get; set; } = DateTime.UtcNow;
		public List<TagDto>? Tags { get; set; }
	}

}
