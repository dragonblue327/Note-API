using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Queries.GetNoteById;
using Note.Application.Notes.Queries.GetNotes;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class GetAllController : NoteControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var notes = await Sender.Send(new GetNoteQuery());
			return Ok(notes);
		}
	}
}
