using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateNote
{
	public class UpdateNoteCommand : IRequest<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
	}
}
