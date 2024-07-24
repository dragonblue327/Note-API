using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateReminder
{
	public class CreateReminderCommand : IRequest<ReminderVm>
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public DateTime ReminderTime { get; set; }
		public List<Tag> Tags { get; set; }
	}
}
