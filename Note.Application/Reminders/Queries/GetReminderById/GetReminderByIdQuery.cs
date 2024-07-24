using MediatR;
using Note.Application.Notes.Queries.GetReminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetReminderById
{
	public class GetReminderByIdQuery : IRequest<ReminderVm>
	{
        public int ReminderId { get; set; }	
    }
}
