using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetTagById
{
	public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagVm>
	{
		private readonly INoteRepository _noteRepository;
		private readonly IMapper _mapper;

		public GetTagByIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
		{
			this._noteRepository = noteRepository;
			this._mapper = mapper;
		}
		public async Task<TagVm> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
		{
			var note = await _noteRepository.GetByIdAsync(request.TagId);
			return _mapper.Map<TagVm>(note);

		}
	}
}
