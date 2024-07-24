using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Note.API.Controllers;
using Note.Application.Notes.Commands.CreateNote;
using Note.Application.Notes.Commands.UpdateNote;
using Note.Application.Notes.Queries.GetNoteById;

namespace TestNoteProjcet.ControllersTests
{
	public class NoteControllerTests
	{
		private readonly Mock<ISender> _mockSender;
		private readonly ISender _sender;
		private readonly NoteController _controller;

		public NoteControllerTests()
		{
			_mockSender = new Mock<ISender>();
			_controller = new NoteController(_mockSender.Object);
			_sender = new Mock<ISender>().Object;
			var httpContext = new DefaultHttpContext();
			httpContext.RequestServices = new ServiceCollection()
				.AddSingleton(_mockSender.Object)
				.BuildServiceProvider();

			_controller.ControllerContext = new ControllerContext
			{
				HttpContext = httpContext
			};
		}
	

[Fact]
		public async Task Create_ShouldReturnCreatedAtAction()
		{
			// Arrange
			var command = new CreateNoteCommand { Title = "Test Note", Text = "Test Text" };
			var createdNote = new NoteDto { Id = 1, Title = "Test Note", Text = "Test Text" };
			_mockSender.Setup(sender => sender.Send(It.IsAny<CreateNoteCommand>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(createdNote);

			// Act
			var result = await _controller.Create(command);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnedNote = Assert.IsType<NoteDto>(createdAtActionResult.Value);
			Assert.Equal(createdNote.Id, returnedNote.Id);
			Assert.Equal(createdNote.Title, returnedNote.Title);
			Assert.Equal(createdNote.Text, returnedNote.Text);
		}

		[Fact]
		public async Task Delete_ShouldReturnNoContent()
		{
			// Arrange
			var controller = new NoteController(_sender);

			// Act
			var result = await controller.Delete(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}


		[Fact]
		public async Task GetNoteById_ShouldReturnOk_WhenNoteExists()
		{
			// Arrange
			var id = 1;
			var note = new NoteDto { Id = id, Title = "Test Note", Text = "Test Text" };
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetNoteByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(note);

			// Act
			var result = await _controller.GetNoteById(id);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedNote = Assert.IsType<NoteDto>(okResult.Value);
			Assert.Equal(note.Id, returnedNote.Id);
			Assert.Equal(note.Title, returnedNote.Title);
			Assert.Equal(note.Text, returnedNote.Text);
		}

		[Fact]
		public async Task GetNoteById_ShouldReturnNotFound_WhenNoteDoesNotExist()
		{
			// Arrange
			var id = 1;
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetNoteByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync((NoteDto)null);

			// Act
			var result = await _controller.GetNoteById(id);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
		{
			// Arrange
			var command = new UpdateNoteCommand { Id = 2, Title = "Updated Note", Text = "Updated Content" };

			// Act
			var result = await _controller.Update(1, command);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}
	}
}



