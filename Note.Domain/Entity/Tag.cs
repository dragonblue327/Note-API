namespace Note.Domain.Entity
{
	public partial class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Note>? Notes { get; set; }
		public List<Reminder>? Reminders { get; set; }
	}
}
