using MediatR;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Queries.GetTagById
{
	public class GetTagByIdQuery : IRequest<TagVm>
	{
        public int TagId { get; set; }	
    }
}
