using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Queries.GetNoteById;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class GetByIdController : NoteControllerBase
	{
		[HttpGet("id")]
		public async Task<IActionResult> GetNoteById(int id)
		{
			var note = await Sender.Send(new GetNoteByIdQuery() { NoteId = id });
			if (note != null)
			{
				return Ok(note);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
