using Note.Application.Common.Mappings;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetReminders
{
	public class ReminderVm : IMapForm<Reminder>
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public DateTime ReminderTime { get; set; } = DateTime.Now;

		public List<Tag>? Tags { get; set; } = new List<Tag>();
	}
}
