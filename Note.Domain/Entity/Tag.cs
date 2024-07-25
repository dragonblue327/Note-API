using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Note.Domain.Entity
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		[IgnoreDataMember]
		public ICollection<Note>? Notes { get; set; } =new List<Note>();
		[JsonIgnore]
		[IgnoreDataMember]
		public ICollection<Reminder>? Reminders { get; set; } =new List<Reminder>();
	}
}
