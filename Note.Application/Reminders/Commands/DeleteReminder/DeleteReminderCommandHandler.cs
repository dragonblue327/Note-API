using MediatR;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.DeleteReminder
{
	public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, int>
	{
		private readonly IReminderRepository _reminderRepository;

		public DeleteReminderCommandHandler(IReminderRepository reminderRepository)
		{
			this._reminderRepository = reminderRepository;
		}

		public async Task<int> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
		{
			try
			{
				return await _reminderRepository.DeleteAsync(request.Id);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

