using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetNoteById;
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
	public class GetNoteByIdQueryHandlerTests
	{
		private readonly Mock<INoteRepository> _mockNoteRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetNoteByIdQueryHandler _handler;

		public GetNoteByIdQueryHandlerTests()
		{
			_mockNoteRepository = new Mock<INoteRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetNoteByIdQueryHandler(_mockNoteRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnNoteVm_WhenNoteExists()
		{
			// Arrange
			var query = new GetNoteByIdQuery { NoteId = 1 };

			var noteEntity = new Note.Domain.Entity.Note
			{
				Id = 1,
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			var noteVm = new NoteVm
			{
				Id = 1,
				Title = "Test Note",
				Text = "Test Text",
				Tags = new List<Tag> { new Tag { Id = 1, Name = "Test Tag" } }
			};

			_mockNoteRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
							   .ReturnsAsync(noteEntity);

			_mockMapper.Setup(mapper => mapper.Map<NoteVm>(It.IsAny<Note.Domain.Entity.Note>()))
					   .Returns(noteVm);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

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
			var query = new GetNoteByIdQuery { NoteId = 1 };

			_mockNoteRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
							   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
