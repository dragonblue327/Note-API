using AutoMapper;
using Moq;
using Note.Application.Notes.Commands.UpdateTag;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNoteProjcet.ApplicationTests.TagsCommandsTests
{
	public class UpdateTagCommandHandlerTests
	{
		private readonly Mock<ITagRepository> _mockTagRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly UpdateTagCommandHandler _handler;

		public UpdateTagCommandHandlerTests()
		{
			_mockTagRepository = new Mock<ITagRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new UpdateTagCommandHandler(_mockTagRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnTagVm_WhenTagIsUpdated()
		{
			// Arrange
			var command = new UpdateTagCommand
			{
				Id = 1,
				Name = "Updated Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Updated Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Updated Reminder" } }
			};

			var tagEntity = new Tag
			{
				Id = 1,
				Name = "Updated Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Updated Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Updated Reminder" } }
			};

			var tagVm = new TagVm
			{
				Id = 1,
				Name = "Updated Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Updated Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Updated Reminder" } }
			};

			_mockTagRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Tag>()))
							  .ReturnsAsync(tagEntity);

			_mockMapper.Setup(mapper => mapper.Map<TagVm>(It.IsAny<Tag>()))
					   .Returns(tagVm);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(tagVm.Id, result.Id);
			Assert.Equal(tagVm.Name, result.Name);
			Assert.Equal(tagVm.Notes.Count, result.Notes.Count);
			Assert.Equal(tagVm.Reminders.Count, result.Reminders.Count);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new UpdateTagCommand
			{
				Id = 1,
				Name = "Updated Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Updated Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Updated Reminder" } }
			};

			_mockTagRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Tag>()))
							  .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
