using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetNoteById
{
	public class GetNoteByIdQuery : IRequest<NoteVm>
	{
        public int NoteId { get; set; }	
    }
}
