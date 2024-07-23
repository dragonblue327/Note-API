using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.DeleteNote;
using Note.Application.Notes.Commands.UpdateNote;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class DeleteController : NoteControllerBase
	{
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await Sender.Send(new DeleteNoteCommand { Id = id });
			return NoContent();
		}
	}
}
