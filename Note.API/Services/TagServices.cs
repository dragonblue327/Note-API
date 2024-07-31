using Note.Application.Notes.Queries.GetTags;
using Note.API.Entity;

namespace Note.API.Services;

public class TagService
{
	public static List<TagDto> ConvertTempDataToTags(List<TagVm> tempData)
	{
		List<TagDto> tags = new List<TagDto>();

		foreach (var temp in tempData)
		{
			
				var tempTag = new TagDto
				{
					Id = temp.Id,
					Name = temp.Name,
					Notes= new List<NoteDto>(),
					Reminders= new List<ReminderDto>(),
				};
			if (temp.Notes != null)
			{
				foreach (var note in temp.Notes!)
				{
					var tempNote = new NoteDto
					{
						Id = note.Id,
						Text = note.Text,
						Title = note.Title
					};
					tempTag.Notes.Add(tempNote);
				}
			}
			if (temp.Reminders != null)
			{
				foreach (var reminder in temp.Reminders!)
				{
					var tempReminder = new ReminderDto
					{
						Id = reminder.Id,
						Title = reminder.Title,
						Text = reminder.Text,
						ReminderTime = reminder.ReminderTime
					};
					tempTag.Reminders!.Add(tempReminder);
				}
			}
			tags.Add(tempTag);
		}

		return tags;
	}
	public static TagDto ConvertTempDataToTag(TagVm tempData)
	{
		var tempTag = new TagDto
		{
			Id = tempData.Id,
			Name = tempData.Name,
			Notes = new List<NoteDto>(),
			Reminders = new List<ReminderDto>()
		};

		if (tempData.Notes != null)
		{
			foreach (var note in tempData.Notes!)
			{
				var tempNote = new NoteDto
				{
					Id = note.Id,
					Text = note.Text,
					Title = note.Title
				};
				tempTag.Notes.Add(tempNote);
			}
		}

		if (tempData.Reminders != null)
		{
			foreach (var reminder in tempData.Reminders!)
			{
				var tempReminder = new ReminderDto
				{
					Id = reminder.Id,
					Title = reminder.Title,
					Text = reminder.Text,
					ReminderTime = reminder.ReminderTime
				};
				tempTag.Reminders!.Add(tempReminder);
			}
		}

		return tempTag;
	}

}
