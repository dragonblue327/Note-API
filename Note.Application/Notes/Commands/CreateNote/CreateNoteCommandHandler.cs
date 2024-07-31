using AutoMapper;
using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteVm>
	{
		private readonly INoteRepository _noteRepository;
		private readonly IMapper _mapper;

		public CreateNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
		{
			this._noteRepository = noteRepository;
			this._mapper = mapper;
		}

		public async Task<NoteVm> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var noteEntity = new Domain.Entity.Note()
				{
					Title = request.Title,
					Text = request.Text,
					Tags = request.Tags ?? new List<Tag>()
				};
				var result = await _noteRepository.CreateAsync(noteEntity);
				return _mapper.Map<NoteVm>(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}

