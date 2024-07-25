using Note.Application.Common.Mappings;
using Note.Domain.Entity;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Note.Application.Notes.Queries.GetNotes
{
	public class NoteVm : IMapForm<Domain.Entity.Note>
	{

		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Text { get; set; } = string.Empty;

		public List<Tag>? Tags { get; set; } = new List<Tag>();
	}
}
