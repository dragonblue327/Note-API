using Note.Application.Common.Mappings;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetTags
{
	public class TagVm : IMapForm<Tag>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[JsonIgnore]
		[IgnoreDataMember]
		public List<Domain.Entity.Note>? Notes { get; set; } = new List<Domain.Entity.Note>();
		[JsonIgnore]
		[IgnoreDataMember]
		public List<Reminder>? Reminders { get; set; } = new List<Reminder>();
	}
}
