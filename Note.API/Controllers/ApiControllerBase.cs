using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Note.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public abstract class NoteControllerBase : ControllerBase
	{
		private ISender _sender;
		protected ISender Sender => _sender ??=  HttpContext.RequestServices.GetService<ISender>();
	}
}
