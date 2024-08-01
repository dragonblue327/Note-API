using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Note.Domain.Entity.Tag;

namespace Note.Domain.Entity
{
	public class Note
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		[JsonIgnore]
		public ICollection<Tag>? Tags { get; set; } 
	}

}
