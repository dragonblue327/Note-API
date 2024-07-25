using MediatR;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.DeleteNote
{
	public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, int>
	{
		private readonly INoteRepository _noteRepository;

		public DeleteNoteCommandHandler(INoteRepository noteRepository)
		{
			this._noteRepository = noteRepository;
		}

		public async Task<int> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
		{
			try
			{
				return await _noteRepository.DeleteAsync(request.Id);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}
