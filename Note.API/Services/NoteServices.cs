using Note.Application.Notes.Queries.GetNotes;
using Note.API.Entity;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Note.Application.Notes.Commands.CreateNote;

namespace Note.API.Services;

public class NoteServices
{
	public static List<NoteDto> ConvertTempDataToNotes(List<NoteVm> tempData) =>
			tempData.Select(temp => new NoteDto
			{
				Id = temp.Id,
				Text = temp.Text,
				Title = temp.Title,
				Tags = temp.Tags?.Select(tag => new TagDto
				{
					Id = tag.Id,
					Name = tag.Name
				}).ToList() ?? new List<TagDto>()
			}).ToList();

	public static NoteDto ConvertTempDataToNote(NoteVm tempData) =>
			new NoteDto
			{
				Id = tempData.Id,
				Text = tempData.Text,
				Title = tempData.Title,
				Tags = tempData.Tags?.Select(tag => new TagDto
				{
					Id = tag.Id,
					Name = tag.Name
				}).ToList() ?? new List<TagDto>()
			};
}
