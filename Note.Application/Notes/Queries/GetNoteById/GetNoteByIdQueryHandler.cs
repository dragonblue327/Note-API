using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Repository;

namespace Note.Application.Notes.Queries.GetNoteById
{
	public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteVm>
	{
		private readonly INoteRepository _noteRepository;
		private readonly IMapper _mapper;

		public GetNoteByIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
		{
			this._noteRepository = noteRepository;
			this._mapper = mapper;
		}

		public async Task<NoteVm> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var note = await _noteRepository.GetByIdAsync(request.NoteId);
				return _mapper.Map<NoteVm>(note);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

