using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Note.Domain.Entity
{
	public class Reminder
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public DateTime ReminderTime { get; set; } = DateTime.Now;
		[JsonIgnore]
		[IgnoreDataMember]
		public List<Tag>? Tags { get; set; }= new List<Tag>();
	}

}
