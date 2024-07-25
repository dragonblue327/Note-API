using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Note.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ServiceFilter(typeof(CustomExceptionFilter))]
	public abstract class ApiControllerBase : ControllerBase
	{
		private ISender _sender;
		protected ISender Sender => _sender ??=  HttpContext.RequestServices.GetService<ISender>();
	}
}
