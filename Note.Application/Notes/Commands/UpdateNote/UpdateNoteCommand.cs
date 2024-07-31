using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Entity;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Note.Application.Notes.Commands.UpdateNote
{
	public class UpdateNoteCommand : IRequest<NoteVm>
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;
		public List<Tag>? Tags { get; set; }
	}
}
