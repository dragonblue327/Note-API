using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Note.Application.Notes.Queries.GetReminder
{
	public class GetReminderQueryHandler : IRequestHandler<GetReminderQuery, List<ReminderVm>>
	{
		private readonly IReminderRepository _reminderRepository;
		private readonly IMapper _mapper;

		public GetReminderQueryHandler(IReminderRepository reminderRepository, IMapper mapper)
		{
			this._reminderRepository = reminderRepository;
			this._mapper = mapper;
		}
		public async Task<List<ReminderVm>> Handle(GetReminderQuery request, CancellationToken cancellationToken)
		{
			var reminders = await _reminderRepository.GetAllNotesAsync();
			var reminderList = _mapper.Map<List<ReminderVm>>(reminders);
			return reminderList;
		}
	}
}
