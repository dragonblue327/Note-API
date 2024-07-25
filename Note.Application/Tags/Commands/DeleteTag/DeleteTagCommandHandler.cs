using MediatR;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.DeleteTag
{
	public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, int>
	{
		private readonly ITagRepository _tagRepository;

		public DeleteTagCommandHandler(ITagRepository tagRepository)
		{
			this._tagRepository = tagRepository;
		}
		public async Task<int> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
		{
			try
			{
				return await _tagRepository.DeleteAsync(request.Id);
			}
			catch (Exception ex)
			{ 
				throw new Exception($"An error occurred: {ex.Message}", ex);
			}
		}
	}
}
