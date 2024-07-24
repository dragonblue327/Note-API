﻿using AutoMapper;
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
		private readonly ITagRepository _tagRepository;
		private readonly IMapper _mapper;

		public GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
		{
			this._tagRepository = tagRepository;
			this._mapper = mapper;
		}
		public async Task<TagVm> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
		{
			var note = await _tagRepository.GetByIdAsync(request.TagId);
			return _mapper.Map<TagVm>(note);

		}
	}
}
