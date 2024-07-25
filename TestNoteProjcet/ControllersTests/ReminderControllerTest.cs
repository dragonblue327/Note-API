using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Note.API.Controllers;
using Note.Application.Notes.Commands.CreateReminder;
using Note.Application.Notes.Commands.UpdateReminder;
using Note.Application.Notes.Queries.GetReminderById;
using Note.Application.Notes.Queries.GetReminders;
using Note.Domain.Entity;
using Xunit;

namespace TestNoteProjcet.ControllersTests
{
	public class ReminderControllerTests
	{
		private readonly Mock<ISender> _mockSender;
		private readonly ISender _sender;
		private readonly ReminderController _controller;

		public ReminderControllerTests()
		{
			_mockSender = new Mock<ISender>();
			_controller = new ReminderController(_mockSender.Object);
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
			var command = new CreateReminderCommand { Title = "Test Note", Text = "Test Text" , ReminderTime = DateTime.Now};
			var createdReminder = new ReminderDto { Id = 1, Title = "Test Note", Text = "Test Text", ReminderTime = DateTime.Now };
			_mockSender.Setup(sender => sender.Send(It.IsAny<CreateReminderCommand>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(createdReminder);

			// Act
			var result = await _controller.Create(command);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnedNote = Assert.IsType<ReminderDto>(createdAtActionResult.Value);
			Assert.Equal(createdReminder.Id, returnedNote.Id);
			Assert.Equal(createdReminder.Title, returnedNote.Title);
			Assert.Equal(createdReminder.Text, returnedNote.Text);
		}
		[Fact]
		public async Task Delete_ShouldReturnNoContent()
		{
			// Arrange
			var controller = new ReminderController(_sender);

			// Act
			var result = await controller.Delete(1);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		[Fact]
		public async Task GetReminderById_ShouldReturnOk_WhenReminderExists()
		{
			// Arrange
			var id = 1;
			var reminder = new ReminderDto { Id = id , Title = "Test Reminder", Text = "Test Reminder" };
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetReminderByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync(reminder);

			// Act
			var result = await _controller.GetReminderById(id);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedReminder = Assert.IsType<ReminderDto>(okResult.Value);
			Assert.Equal(reminder.Id, returnedReminder.Id);
			Assert.Equal(reminder.Title, returnedReminder.Title);
			Assert.Equal(reminder.Text, returnedReminder.Text);
		}

		[Fact]
		public async Task GetReminderById_ShouldReturnNotFound_WhenReminderDoesNotExist()
		{
			// Arrange
			var id = 1;
			_mockSender.Setup(sender => sender.Send(It.IsAny<GetReminderByIdQuery>(), It.IsAny<CancellationToken>()))
					   .ReturnsAsync((ReminderDto)null);

			// Act
			var result = await _controller.GetReminderById(id);

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}

		[Fact]
		public async Task Update_ShouldReturnBadRequest_WhenIdMismatch()
		{
			// Arrange
			var command = new UpdateReminderCommand { Id = 2};

			// Act
			var result = await _controller.Update(1, command);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}
	}
}
