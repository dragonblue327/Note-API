using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Note.API.Controllers;
using Note.Application.Notes.Commands.CreateTag;
using Note.Application.Notes.Commands.DeleteTag;
using Note.Application.Notes.Commands.UpdateTag;
using Note.Application.Notes.Queries.GetTagById;



namespace TestNoteProjcet.ControllersTests
{
	public class TagControllerTests
	{
		private readonly Mock<ISender> _mockSender;
		private readonly ISender _sender;
		private readonly TagController _controller;

		public TagControllerTests()
		{
			_mockSender = new Mock<ISender>();
			_controller = new TagController(_mockSender.Object);
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
			var command = new CreateTagCommand { Name = "Test Tag" };
			var createdTag = new TagDto { Id = 1, Name = "Test Tag" };
			_mockSender.Setup(sender => sender.Send(It.IsAny<CreateTagCommand>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(createdTag);

			// Act
			var result = await _controller.Create(command);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnedTag = Assert.IsType<TagDto>(createdAtActionResult.Value);
			Assert.Equal(createdTag.Id, returnedTag.Id);
			Assert.Equal(createdTag.Name, returnedTag.Name);
		}

		[Fact]
		public async Task Delete_ShouldReturnNoContent()
		{
			// Arrange
			var controller = new TagController(_sender);

			// Act
			var result = await controller.Delete(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task GetTagById_ShouldReturnOk_WhenTagExists()
		{
			// Arrange
			var id = 1;
			var tag = new TagDto { Id = id, Name = "Test Tag" };
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetTagByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(tag);

			// Act
			var result = await _controller.GetTagById(id);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedTag = Assert.IsType<TagDto>(okResult.Value);
			Assert.Equal(tag.Id, returnedTag.Id);
			Assert.Equal(tag.Name, returnedTag.Name);
		}

		[Fact]
		public async Task GetTagById_ShouldReturnNotFound_WhenTagDoesNotExist()
		{
			// Arrange
			var id = 1;
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetTagByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync((TagDto)null);

			// Act
			var result = await _controller.GetTagById(id);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
		{
			// Arrange
			var command = new UpdateTagCommand { Id = 2, Name = "Updated Tag" };

			// Act
			var result = await _controller.Update(1, command);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}
	}

	
}
