using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
		[HttpPost("Create")]
		public async Task<IActionResult> Create(CreateTagCommand command)
		{
			try
			{
				var createdTag = await Sender.Send(command);
				return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, createdTag);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while creating the tag: {ex.Message}", ex);
			}
		}

		[HttpDelete("Delete")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await Sender.Send(new DeleteTagCommand { Id = id });
				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while deleting the tag: {ex.Message}", ex);
			}
		}

		[HttpGet("GetById")]
		public async Task<IActionResult> GetTagById(int id)
		{
			try
			{
				var tag = await Sender.Send(new GetTagByIdQuery { TagId = id });
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

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				var tags = await Sender.Send(new GetTagQuery());
				return Ok(tags);
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while retrieving the tags: {ex.Message}", ex);
			}
		}

		[HttpPut("{UpdateById}")]
		public async Task<IActionResult> Update(int id, UpdateTagCommand command)
		{
			try
			{
				if (id != command.Id)
				{
					return BadRequest();
				}
				await Sender.Send(command);
				return NoContent();
			}
			catch (Exception ex)
			{
				throw new Exception($"An error occurred while updating the tag: {ex.Message}", ex);
			}
		}
	}
}

