using Note.API.Entity;
using Note.Application.Notes.Queries.GetReminders;

namespace Note.API.Services
{
	public class ReminderService
	{
		public static List<ReminderDto> ConvertTempDataToReminders(List<ReminderVm> tempData) =>
			tempData.Select(temp => new ReminderDto
			{
				Id = temp.Id,
				Title = temp.Title,
				Text = temp.Text,
				ReminderTime = temp.ReminderTime,
				Tags = temp.Tags?.Select(tag => new TagDto
				{
					Id = tag.Id,
					Name = tag.Name
				}).ToList() ?? new List<TagDto>()
				}).ToList();

		public static ReminderDto ConvertTempDataToReminder(ReminderVm tempData) =>
				new ReminderDto
				{
					Id = tempData.Id,
					Title = tempData.Title,
					Text = tempData.Text,
					ReminderTime = tempData.ReminderTime,
					Tags = tempData.Tags?.Select(tag => new TagDto
					{
						Id = tag.Id,
						Name = tag.Name
					}).ToList() ?? new List<TagDto>()
				};



	}

}
