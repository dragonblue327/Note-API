using MediatR;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;

namespace Note.Application.Notes.Commands.CreateTag
{
	public class CreateTagCommand : IRequest<TagVm>
	{
		public string Name { get; set; }
		public List<Domain.Entity.Note>? Notes { get; set; }
		public List<Reminder>? Reminders { get; set; }
	}
}
