using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;

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
			try
			{
				var tagEntity = new Tag()
				{
					Name = request.Name,
					Notes = request.Notes ?? new List<Domain.Entity.Note>(),
					Reminders = request.Reminders ?? new List<Reminder>(),
				};
				var result = await _tagRepository.CreateAsync(tagEntity);
				return _mapper.Map<TagVm>(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

