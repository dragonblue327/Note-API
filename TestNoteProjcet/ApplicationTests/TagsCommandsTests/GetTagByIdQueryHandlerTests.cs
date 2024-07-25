using AutoMapper;
using Moq;
using Note.Application.Notes.Queries.GetNotes;
using Note.Application.Notes.Queries.GetReminders;
using Note.Application.Notes.Queries.GetTagById;
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
	public class GetTagByIdQueryHandlerTests
	{
		private readonly Mock<ITagRepository> _mockTagRepository;
		private readonly Mock<IMapper> _mockMapper;
		private readonly GetTagByIdQueryHandler _handler;

		public GetTagByIdQueryHandlerTests()
		{
			_mockTagRepository = new Mock<ITagRepository>();
			_mockMapper = new Mock<IMapper>();
			_handler = new GetTagByIdQueryHandler(_mockTagRepository.Object, _mockMapper.Object);
		}

		[Fact]
		public async Task Handle_ShouldReturnTagVm_WhenTagExists()
		{
			// Arrange
			var query = new GetTagByIdQuery { TagId = 1 };

			var tagEntity = new Tag
			{
				Id = 1,
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			var tagVm = new TagVm
			{
				Id = 1,
				Name = "Test Tag",
				Notes = new List<Note.Domain.Entity.Note> { new Note.Domain.Entity.Note { Id = 1, Title = "Test Note" } },
				Reminders = new List<Reminder> { new Reminder { Id = 1, Title = "Test Reminder" } }
			};

			_mockTagRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
							  .ReturnsAsync(tagEntity);

			_mockMapper.Setup(mapper => mapper.Map<TagVm>(It.IsAny<Tag>()))
					   .Returns(tagVm);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

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
			var query = new GetTagByIdQuery { TagId = 1 };

			_mockTagRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
							  .ThrowsAsync(new Exception("Repository error"));

			// Act & Assert
			var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
			Assert.Equal("An error occurred: Repository error", exception.Message);
		}
	}
}
