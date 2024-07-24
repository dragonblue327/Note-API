using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateReminder
{
	public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ReminderVm>
	{
		private readonly IReminderRepository _reminderRepository;
		private readonly IMapper _mapper;

		public CreateReminderCommandHandler(IReminderRepository reminderRepository, IMapper mapper)
        {
			this._reminderRepository = reminderRepository;
			this._mapper = mapper;
		}
        public async Task<ReminderVm> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
		{
			var reminderEnity = new Reminder()
			{
				Title = request.Title,
				Text = request.Text,
				ReminderTime = request.ReminderTime,
				Tags = request.Tags ?? new List<Tag>(),
			};
			var result = await _reminderRepository.CreateAsync(reminderEnity);
			return _mapper.Map<ReminderVm>(result);
		}
	}
}
