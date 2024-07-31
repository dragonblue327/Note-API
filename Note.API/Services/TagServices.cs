using Note.Application.Notes.Queries.GetTags;
using Note.API.Entity;

namespace Note.API.Services;

public class TagService
{
	public static List<TagDto> ConvertTempDataToTags(List<TagVm> tempData) =>
				tempData.Select(temp => new TagDto
				{
					Id = temp.Id,
					Name = temp.Name,
					Notes = temp.Notes?.Select(note => new NoteDto
					{
						Id = note.Id,
						Text = note.Text,
						Title = note.Title
					}).ToList() ?? new List<NoteDto>(),
					Reminders = temp.Reminders?.Select(reminder => new ReminderDto
					{
						Id = reminder.Id,
						Title = reminder.Title,
						Text = reminder.Text,
						ReminderTime = reminder.ReminderTime
					}).ToList() ?? new List<ReminderDto>()
				}).ToList();

	public static TagDto ConvertTempDataToTag(TagVm tempData) =>
			new TagDto
			{
				Id = tempData.Id,
				Name = tempData.Name,
				Notes = tempData.Notes?.Select(note => new NoteDto
				{
					Id = note.Id,
					Text = note.Text,
					Title = note.Title
				}).ToList() ?? new List<NoteDto>(),
				Reminders = tempData.Reminders?.Select(reminder => new ReminderDto
				{
					Id = reminder.Id,
					Title = reminder.Title,
					Text = reminder.Text,
					ReminderTime = reminder.ReminderTime
				}).ToList() ?? new List<ReminderDto>()
			};


}
