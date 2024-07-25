using MediatR;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;

namespace Note.Application.Notes.Commands.UpdateTag
{
	public class UpdateTagCommand : IRequest<TagVm>
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public List<Domain.Entity.Note>? Notes { get; set; }
		public List<Reminder>? Reminders { get; set; }

	}

}
