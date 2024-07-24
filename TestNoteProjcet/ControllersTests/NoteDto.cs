using Note.Application.Notes.Queries.GetNotes;

namespace TestNoteProjcet.ControllersTests
{
	internal class NoteDto : NoteVm
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
	}
}