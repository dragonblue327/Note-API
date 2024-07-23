using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			var note = await _noteRepository.GetByIdAsync(request.NoteId);
			return _mapper.Map<NoteVm>(note);

		}
	}
}
