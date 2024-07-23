using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommand : IRequest<NoteVm>
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public List<Tag>? Tags { get; set; }
	}
}
