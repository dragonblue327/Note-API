using Note.Application.Notes.Queries.GetReminders;

namespace TestNoteProjcet.ControllersTests
{
	public class ReminderDto : ReminderVm
	{
		public int Id { get; set; }
		public string Title { get; set; } 
		public string Text { get; set; } 
		public DateTime ReminderTime { get; set; } 

	}
}
