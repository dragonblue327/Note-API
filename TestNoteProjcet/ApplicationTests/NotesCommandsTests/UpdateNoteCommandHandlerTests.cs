using AutoMapper;
using Moq;
using Note.Application.Notes.Commands.UpdateNote;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNoteProjcet.ApplicationTests.NotesCommandsTests
{
	public class UpdateNoteCommandHandlerTests
	{
		private readonly Mock<INoteRepository> _mockNoteRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly UpdateNoteCommandHandler _handler;

		public UpdateNoteCommandHandlerTests()
		{
			_mockNoteRepository = new Mock<INoteRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new UpdateNoteCommandHandler(_mockNoteRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnNoteVm_WhenNoteIsUpdated()
		{
			// Arrange
			var command = new UpdateNoteCommand
			{
				Id = 1,
				Title = "Updated Note",
				Text = "Updated Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			var noteEntity = new Note.Domain.Entity.Note
			{
				Id = 1,
				Title = "Updated Note",
				Text = "Updated Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			var noteVm = new NoteVm
			{
				Id = 1,
				Title = "Updated Note",
				Text = "Updated Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			_mockNoteRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Note.Domain.Entity.Note>()))
							   .ReturnsAsync(noteEntity);

			_mockMapper.Setup(mapper => mapper.Map<NoteVm>(It.IsAny<Note.Domain.Entity.Note>()))
					   .Returns(noteVm);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(noteVm.Id, result.Id);
			Assert.Equal(noteVm.Title, result.Title);
			Assert.Equal(noteVm.Text, result.Text);
			Assert.Equal(noteVm.Tags.Count, result.Tags.Count);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var command = new UpdateNoteCommand
			{
				Id = 1,
				Title = "Updated Note",
				Text = "Updated Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Updated Tag" } }
			};

			_mockNoteRepository.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Note.Domain.Entity.Note>()))
							   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
