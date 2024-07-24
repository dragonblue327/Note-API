using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.DeleteReminder
{
	public class DeleteReminderCommand : IRequest<int>
	{
        public int Id { get; set; }
    }
}
