using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetNotes;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.NotesCommandsTests
{
	public class GetNotesQueryHandlerTests
	{
		private readonly Mock<INoteRepository> _mockNoteRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetNoteQueryHandler _handler;

		public GetNotesQueryHandlerTests()
		{
			_mockNoteRepository = new Mock<INoteRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetNoteQueryHandler(_mockNoteRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnListOfNoteVm_WhenNotesExist()
		{
			// Arrange
			var query = new GetNoteQuery();

			var noteEntities = new List<Note.Domain.Entity.Note>
			{
				new Note.Domain.Entity.Note { Id = 1, Title = "Test Note 1", Text = "Test Text 1", Tags = new List<Tag> { new Tag { Id = 1, Name = "Tag1" } } },
				new Note.Domain.Entity.Note { Id = 2, Title = "Test Note 2", Text = "Test Text 2", Tags = new List<Tag> { new Tag { Id = 2, Name = "Tag2" } } }
			};

			var noteVms = new List<NoteVm>
			{
				new NoteVm { Id = 1, Title = "Test Note 1", Text = "Test Text 1", Tags = new List<Tag> { new Tag { Id = 1, Name = "Tag1" } } },
				new NoteVm { Id = 2, Title = "Test Note 2", Text = "Test Text 2", Tags = new List<Tag> { new Tag { Id = 2, Name = "Tag2" } } }
			};

			_mockNoteRepository.Setup(repo => repo.GetAllNotesAsync())
							   .ReturnsAsync(noteEntities);

			_mockMapper.Setup(mapper => mapper.Map<List<NoteVm>>(It.IsAny<List<Note.Domain.Entity.Note>>()))
					   .Returns(noteVms);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(noteVms.Count, result.Count);
			Assert.Equal(noteVms[0].Id, result[0].Id);
			Assert.Equal(noteVms[1].Id, result[1].Id);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var query = new GetNoteQuery();

			_mockNoteRepository.Setup(repo => repo.GetAllNotesAsync())
							   .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
