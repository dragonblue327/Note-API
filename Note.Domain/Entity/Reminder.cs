namespace Note.Domain.Entity
{
	public class Reminder
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public DateTime ReminderTime { get; set; }
		public List<Tag> Tags { get; set; }
	}

}
