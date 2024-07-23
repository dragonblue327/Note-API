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
		[HttpPost]
		public async Task<IActionResult> Create(CreateTagCommand command)
		{
			var createdTag = await Sender.Send(command);
			return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, createdTag);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await Sender.Send(new DeleteTagCommand { Id = id });
			return NoContent();
		}
		[HttpGet("id")]
		public async Task<IActionResult> GetTagById(int id)
		{
			var Tag = await Sender.Send(new GetTagByIdQuery() { TagId = id });
			if (Tag != null)
			{
				return Ok(Tag);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var Tags = await Sender.Send(new GetTagQuery());
			return Ok(Tags);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, UpdateTagCommand command)
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
