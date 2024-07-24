using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Note.Application.Notes.Commands.CreateReminder;
using Note.Application.Notes.Commands.DeleteReminder;
using Note.Application.Notes.Commands.UpdateReminder;
using Note.Application.Notes.Queries.GetReminder;
using Note.Application.Notes.Queries.GetReminderById;
using Note.Application.Notes.Queries.GetTags;

namespace Note.API.Controllers
{
	[Route("Reminder/[controller]")]
	[ApiController]
	public class ReminderController : ApiControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create(CreateReminderCommand command)
		{
			var createdReminder = await Sender.Send(command);
			return CreatedAtAction(nameof(GetReminderById), new { id = createdReminder.Id }, createdReminder);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await Sender.Send(new DeleteReminderCommand { Id = id });
			return NoContent();
		}
		[HttpGet("id")]
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
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var Reminders = await Sender.Send(new GetReminderQuery());
			return Ok(Reminders);
		}
		[HttpPut("{id}")]
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
