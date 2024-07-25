using MediatR;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.CreateNote;
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
			try
			{
				var createdNote = await _sender.Send(command);
				return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while creating the note: {ex.Message}", ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _sender.Send(new DeleteNoteCommand { Id = id });
				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the note: {ex.Message}", ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetNoteById(int id)
		{
			try
			{
				var note = await _sender.Send(new GetNoteByIdQuery { NoteId = id });
				if (note != null)
				{
					return Ok(note);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the note: {ex.Message}", ex);
			}
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				var notes = await _sender.Send(new GetNoteQuery());
				return Ok(notes);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the notes: {ex.Message}", ex);
			}
		}

		[HttpPost("UpdateById")]
		public async Task<IActionResult> Update(int id, UpdateNoteCommand command)
		{
			try
			{
				if (id != command.Id)
				{
					return BadRequest();
				}
				var result = await _sender.Send(command);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the note: {ex.Message}", ex);
			}
		}
	}
}

