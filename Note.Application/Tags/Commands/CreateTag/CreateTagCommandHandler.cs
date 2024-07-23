using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateTag
{
	public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagVm>
	{
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;

		public CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
        {
			this._tagRepository = tagRepository;
			this._mapper = mapper;
		}
        public async Task<TagVm> Handle(CreateTagCommand request, CancellationToken cancellationToken)
		{
			var tagEnity = new Tag()
			{
				Name = request.Name,
				Notes = request.Notes??null,
				Reminders = request.Reminders??null,
			};
			var result = await _tagRepository.CreateAsync(tagEnity);
			return _mapper.Map<TagVm>(result);
		}
	}
}
