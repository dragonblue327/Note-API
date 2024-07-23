using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetTags
{
	public class GetTagQuery : IRequest<List<TagVm>>
	{
	}
}
