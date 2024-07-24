using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.CreateReminder;
using Note.Application.Notes.Commands.DeleteReminder;
using Note.Application.Notes.Commands.UpdateReminder;
using Note.Application.Notes.Queries.GetReminder;
using Note.Application.Notes.Queries.GetReminderById;

namespace Note.API.Controllers
{
	[Route("Reminder/[controller]")]
	[ApiController]
	public class ReminderController : ApiControllerBase
	{
		private readonly ISender _sender;

		public ReminderController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(CreateReminderCommand command)
		{
			var createdReminder = await Sender.Send(command);
			return CreatedAtAction(nameof(GetReminderById), new { id = createdReminder.Id }, createdReminder);
		}
		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			await Sender.Send(new DeleteReminderCommand { Id = id });
			return NoContent();
		}
		[HttpGet("GetById")]
		public async Task<IActionResult> GetReminderById(int id)
		{
			var reminder = await Sender.Send(new GetReminderByIdQuery() { ReminderId = id });
			if (reminder != null)
			{
				return Ok(reminder);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			var Reminders = await Sender.Send(new GetReminderQuery());
			return Ok(Reminders);
		}
		[HttpPut("{UpdateById}")]
		public async Task<IActionResult> Update(int id, UpdateReminderCommand command)
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
