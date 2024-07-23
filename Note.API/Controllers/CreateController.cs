using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.CreateNote;
using Note.Application.Notes.Commands.UpdateNote;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class CreateController : NoteControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateNoteCommand command)
		{
			var createdNote = await Sender.Send(command);
			return CreatedAtAction(nameof(GetByIdController.GetNoteById), new { id = createdNote.Id }, createdNote);
		}


	
	}
}
