using System.Text.Json.Serialization;

namespace Note.Domain.Entity
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		[JsonIgnore]
		public ICollection<Note>? Notes { get; set; }
		[JsonIgnore]
		public ICollection<Reminder>? Reminders { get; set; }
	}
}
