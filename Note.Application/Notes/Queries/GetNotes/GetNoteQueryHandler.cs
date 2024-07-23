using AutoMapper;
using MediatR;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Note.Application.Notes.Queries.GetNotes
{
	public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, List<NoteVm>>
	{
		private readonly INoteRepository _noteRepository;
		private readonly IMapper _mapper;

		public GetNoteQueryHandler(INoteRepository noteRepository , IMapper mapper)
		{
			this._noteRepository = noteRepository;
			this._mapper = mapper;
		}
		public async Task<List<NoteVm>> Handle(GetNoteQuery request, CancellationToken cancellationToken)
		{
			var notes = await _noteRepository.GetAllNotesAsync();
			//var noteList = notes.Select(x => new NoteVm { Id = x.Id,
			//	Name = x.Name,
			//	Text = x.Text }).ToList();

			var noteList = _mapper.Map<List<NoteVm>>(notes);
			return noteList;
		}
	}
}
