using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.DeleteTag
{
	public class DeleteTagCommand : IRequest<int>
	{
        public int Id { get; set; }
    }
}
