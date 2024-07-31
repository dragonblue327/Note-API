using MediatR;
using Microsoft.AspNetCore.Mvc;
using Note.API.Services;
using Note.Application.Notes.Commands.CreateTag;
using Note.Application.Notes.Commands.DeleteTag;
using Note.Application.Notes.Commands.UpdateTag;
using Note.Application.Notes.Queries.GetTagById;
using Note.Application.Notes.Queries.GetTags;

namespace Note.API.Controllers
{
	[Route("Tag/[controller]")]
	[ApiController]
	public class TagController : ApiControllerBase
	{
		private readonly ISender _sender;

		public TagController(ISender sender)
		{
			_sender = sender;
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create(CreateTagCommand command)
		{
			try
			{
				var createdTag = await _sender.Send(command);
				return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, createdTag);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while creating the tag: {ex.Message}", ex);
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _sender.Send(new DeleteTagCommand { Id = id });
				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the tag: {ex.Message}", ex);
			}
		}

		[HttpPost("GetById")]
		public async Task<IActionResult> GetTagById(int id)
		{
			try
			{
				var tag = TagService.ConvertTempDataToTag( await _sender.Send(new GetTagByIdQuery { TagId = id }));
				if (tag != null)
				{
					return Ok(tag);
				}
				else
				{
					return NotFound();
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the tag: {ex.Message}", ex);
			}
		}

		[HttpPost("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				var tags = await _sender.Send(new GetTagQuery());
				var result = TagService.ConvertTempDataToTags(tags);
				return Ok(result);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the tags: {ex.Message}", ex);
			}
		}
		[HttpPost("UpdateById")]
		public async Task<IActionResult> Update(int id, UpdateTagCommand command)
		{
			try
			{
				if (id != command.Id)
				{
					return BadRequest();
				}
				var tag = TagService.ConvertTempDataToTag( await _sender.Send(command));
				return Ok(tag);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the tag: {ex.Message}", ex);
			}
		}


		
	}
}

