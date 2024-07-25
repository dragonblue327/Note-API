using AutoMapper;
using MediatR;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateReminder
{
	public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, int>
	{
		private readonly IReminderRepository _reminderRepository;

		public UpdateReminderCommandHandler(IReminderRepository reminderRepository)
		{
			this._reminderRepository = reminderRepository;
		}

		public async Task<int> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
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
				return await _reminderRepository.UpdateAsync(request.Id, updateReminderEntity);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

