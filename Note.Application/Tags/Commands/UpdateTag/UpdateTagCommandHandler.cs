using AutoMapper;
using MediatR;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.UpdateTag
{
	public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, int>
	{
		private readonly ITagRepository _tagRepository;

		public UpdateTagCommandHandler(ITagRepository tagRepository)
		{
			this._tagRepository = tagRepository;
		}
		public async Task<int> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var updateNoteEntity = new Tag()
				{
					Id = request.Id,
					Name = request.Name,
					Notes = request.Notes,
					Reminders = request.Reminders ?? null,
				};
				return await _tagRepository.UpdateAsync(request.Id, updateNoteEntity);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
	
		}
	}
}
