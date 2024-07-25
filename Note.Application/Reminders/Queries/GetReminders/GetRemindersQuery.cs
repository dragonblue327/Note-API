using MediatR;
using Note.Application.Notes.Queries.GetReminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetReminder
{
	public class GetReminderQuery : IRequest<List<ReminderVm>>
	{
	}
}
