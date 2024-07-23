using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.UpdateNote;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class UpdateController : NoteControllerBase
	{
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateNoteCommand command)
		{
			if (id != command.Id)
			{
				return BadRequest();
			}
			await Sender.Send(command);
			return NoContent();

		}
	}
}
