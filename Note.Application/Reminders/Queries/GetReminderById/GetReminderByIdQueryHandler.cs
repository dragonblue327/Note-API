using AutoMapper;
using MediatR;
using Note.Application.Common.Exceptions;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetReminderById
{
	public class GetReminderByIdQueryHandler : IRequestHandler<GetReminderByIdQuery, ReminderVm>
	{
		private readonly IReminderRepository _reminderRepository;
		private readonly IMapper _mapper;

		public GetReminderByIdQueryHandler(IReminderRepository reminderRepository, IMapper mapper)
		{
			this._reminderRepository = reminderRepository;
			this._mapper = mapper;
		}

		public async Task<ReminderVm> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var note = await _reminderRepository.GetByIdAsync(request.ReminderId);
				return _mapper.Map<ReminderVm>(note);
			}
			catch (NotFoundEntityException ex)
			{
				throw new NotFoundEntityException($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

