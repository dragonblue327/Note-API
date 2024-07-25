using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetReminders;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateTag
{
	public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, TagVm>
	{
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;


		public UpdateTagCommandHandler(ITagRepository tagRepository , IMapper mapper)
		{
			this._tagRepository = tagRepository;
			this._mapper = mapper;
		}
		public async Task<TagVm> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var updateNoteEntity = new Tag()
				{
					Id = request.Id,
					Name = request.Name,
					Notes = request.Notes ?? new List<Domain.Entity.Note>(),
					Reminders = request.Reminders ?? new List<Reminder>(),
				};
				var result =  await _tagRepository.UpdateAsync(request.Id, updateNoteEntity) ;
				return _mapper.Map<TagVm>(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
	
		}
	}
}
