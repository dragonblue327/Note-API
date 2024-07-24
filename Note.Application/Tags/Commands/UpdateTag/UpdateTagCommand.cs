using MediatR;
using Note.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateTag
{
	public class UpdateTagCommand : IRequest<int>
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public List<Domain.Entity.Note> Notes { get; set; }
		public List<Reminder>? Reminders { get; set; }
	}
}
