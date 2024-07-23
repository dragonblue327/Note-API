using AutoMapper;
using MediatR;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateNote
{
	public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, int>
	{
		private readonly INoteRepository _noteRepository;

		public UpdateNoteCommandHandler(INoteRepository noteRepository)
		{
			this._noteRepository = noteRepository;
		}
		public async Task<int> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
		{
			var updateNoteEntity = new Domain.Entity.Note()
			{
				Id = request.Id,
				Title = request.Title,
				Text = request.Text
			};
			return await _noteRepository.UpdateAsync(request.Id, updateNoteEntity);
	
		}
	}
}
