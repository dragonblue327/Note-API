using Note.Application.Common.Mappings;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetNotes
{
	public class NoteVm : IMapForm<Domain.Entity.Note>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
	}
}
