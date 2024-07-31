using Note.Application.Notes.Queries.GetNotes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Note.API.Entity
{
	public class NoteDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public string Text { get; set; } = "";
		public ICollection<TagDto>? Tags { get; set; } 
	}
	

}
