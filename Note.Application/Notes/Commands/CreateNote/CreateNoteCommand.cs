using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommand : IRequest<NoteVm>
	{
        public string Name { get; set; }
		public string Text { get; set; }
	}
}
