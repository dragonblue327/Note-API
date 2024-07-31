using MediatR;
using Microsoft.AspNetCore.Mvc;
using Note.API.Services;
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
			try
			{
				var createdReminder = await _sender.Send(command);
				return CreatedAtAction(nameof(GetReminderById), new { id = createdReminder.Id }, createdReminder);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while creating the reminder: {ex.Message}", ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _sender.Send(new DeleteReminderCommand { Id = id });
				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the reminder: {ex.Message}", ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetReminderById(int id)
		{
			try
			{
				var reminder = ReminderService.ConvertTempDataToReminder(await _sender.Send(new GetReminderByIdQuery { ReminderId = id }));
				if (reminder != null)
				{
					return Ok(reminder);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the reminder: {ex.Message}", ex);
			}
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				var reminders = await _sender.Send(new GetReminderQuery());
				var result = ReminderService.ConvertTempDataToReminders(reminders);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the reminders: {ex.Message}", ex);
			}
		}

		[HttpPost("UpdateById")]
		public async Task<IActionResult> Update(int id, UpdateReminderCommand command)
		{
			try
			{
				if (id != command.Id)
				{
					return BadRequest();
				}
				var result =ReminderService.ConvertTempDataToReminder(await _sender.Send(command));
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the reminder: {ex.Message}", ex);
			}
		}
	}
}

