using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommand : IRequest<NoteVm>
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public List<Tag>? Tags { get; set; }
	}
}
