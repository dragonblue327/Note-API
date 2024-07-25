

using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Application.Notes.Queries.GetTags;
using Note.Domain.Entity;
using Note.Domain.Repository;

namespace TestNoteProjcet.ApplicationTests.NotesCommandsTests
{
	public class GetTagQueryHandlerTests
	{
		private readonly Mock<ITagRepository> _mockTagRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetTagQueryHandler _handler;

		public GetTagQueryHandlerTests()
		{
			_mockTagRepository = new Mock<ITagRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetTagQueryHandler(_mockTagRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnListOfTagVm_WhenTagsExist()
		{
			// Arrange
			var query = new GetTagQuery();

			var tagEntities = new List<Tag>
			{
				new Tag { Id = 1, Name = "Tag1", Notes = new List<Note.Domain.Entity.Note> 
				{ new Note.Domain.Entity.Note { Id = 1, Title = "Note1" } }, Reminders = new List<Reminder> 
				{ new Reminder { Id = 1, Title = "Reminder1" } } },
				new Tag { Id = 2, Name = "Tag2", Notes = new List<Note.Domain.Entity.Note> 
				{ new Note.Domain.Entity.Note { Id = 2, Title = "Note2" } }, Reminders = new List<Reminder> 
				{ new Reminder { Id = 2, Title = "Reminder2" } } }
			};

			var tagVms = new List<TagVm>
			{
				new TagVm { Id = 1, Name = "Tag1", Notes = new List<Note.Domain.Entity.Note> 
				{ new Note.Domain.Entity.Note { Id = 1, Title = "Note1" } }, Reminders = new List<Reminder> 
				{ new Reminder { Id = 1, Title = "Reminder1" } } },
				new TagVm { Id = 2, Name = "Tag2", Notes = new List<Note.Domain.Entity.Note> 
				{ new Note.Domain.Entity.Note { Id = 2, Title = "Note2" } }, Reminders = new List<Reminder> 
				{ new Reminder { Id = 2, Title = "Reminder2" } } }
			};

			_mockTagRepository.Setup(repo => repo.GetAllTagsAsync())
							  .ReturnsAsync(tagEntities);

			_mockMapper.Setup(mapper => mapper.Map<List<TagVm>>(It.IsAny<List<Tag>>()))
					   .Returns(tagVms);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(tagVms.Count, result.Count);
			Assert.Equal(tagVms[0].Id, result[0].Id);
			Assert.Equal(tagVms[1].Id, result[1].Id);
		}

		[Fact]
		public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
		{
			// Arrange
			var query = new GetTagQuery();

			_mockTagRepository.Setup(repo => repo.GetAllTagsAsync())
							  .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
