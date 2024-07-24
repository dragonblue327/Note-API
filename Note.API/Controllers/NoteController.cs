using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Note.Application.Notes.Commands.CreateNote;
using Note.Application.Notes.Commands.CreateTag;
using Note.Application.Notes.Commands.DeleteNote;
using Note.Application.Notes.Commands.UpdateNote;
using Note.Application.Notes.Queries.GetNoteById;
using Note.Application.Notes.Queries.GetNotes;

namespace Note.API.Controllers
{
	[Route("Note/[controller]")]
	[ApiController]
	public class NoteController : ApiControllerBase
	{
		private readonly ISender _sender;

		public NoteController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(CreateNoteCommand command)
		{
			var createdNote = await Sender.Send(command);
			return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
		}
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			await _sender.Send(new DeleteNoteCommand { Id = id });
			return NoContent();
		}

		[HttpGet("GetById")]
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
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			var notes = await _sender.Send(new GetNoteQuery());
			return Ok(notes);
		}
		[HttpPut("{UpdateById}")]
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
