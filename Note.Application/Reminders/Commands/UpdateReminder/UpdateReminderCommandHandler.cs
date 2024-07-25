using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateReminder
{
	public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ReminderVm>
	{
		private readonly IReminderRepository _reminderRepository;
		private readonly IMapper _mapper;

		public UpdateReminderCommandHandler(IReminderRepository reminderRepository, IMapper mapper)
		{
			this._reminderRepository = reminderRepository;
			_mapper = mapper;
		}

		public async Task<ReminderVm> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var updateReminderEntity = new Reminder
				{
					Id = request.Id,
					Title = request.Title,
					Text = request.Text,
					Tags = request.Tags ?? null,
					ReminderTime = request.ReminderTime,
				};
				var result = await _reminderRepository.UpdateAsync(request.Id, updateReminderEntity);
				return _mapper.Map<ReminderVm>(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

