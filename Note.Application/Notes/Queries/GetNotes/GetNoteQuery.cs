using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetNotes
{
	public class GetNoteQuery : IRequest<List<NoteVm>>
	{
	}
}
