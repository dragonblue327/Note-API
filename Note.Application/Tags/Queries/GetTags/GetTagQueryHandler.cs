using AutoMapper;
using MediatR;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Note.Application.Notes.Queries.GetTags
{
	public class GetTagQueryHandler : IRequestHandler<GetTagQuery, List<TagVm>>
	{
		private readonly ITagRepository _TagRepository;
		private readonly IMapper _mapper;

		public GetTagQueryHandler(ITagRepository TagRepository , IMapper mapper)
		{
			this._TagRepository = TagRepository;
			this._mapper = mapper;
		}
		public async Task<List<TagVm>> Handle(GetTagQuery request, CancellationToken cancellationToken)
		{
			var Tags = await _TagRepository.GetAllTagsAsync();
			var TagList = _mapper.Map<List<TagVm>>(Tags);
			return TagList;
		}
	}
}
