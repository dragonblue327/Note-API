using Note.API.Entity;
using Note.Application.Notes.Queries.GetReminders;

namespace Note.API.Services
{
	public class ReminderService
	{
		public static List<ReminderDto> ConvertTempDataToReminders(List<ReminderVm> tempData)
		{
			List<ReminderDto> reminders = new List<ReminderDto>();

			foreach (var temp in tempData)
			{

				var tempReminder = new ReminderDto
				{
					Id = temp.Id,
					Title = temp.Title,
					Text = temp.Text,
					ReminderTime = temp.ReminderTime,
					Tags = new List<TagDto>()
				};

				foreach (var tag in temp.Tags!)
				{
					var tempTag = new TagDto
					{
						Id = tag.Id,
						Name = tag.Name
					};
					tempReminder.Tags!.Add(tempTag);
				}

				reminders.Add(tempReminder);
			}

			return reminders;
		}
		public static ReminderDto ConvertTempDataToReminder(ReminderVm tempData)
		{
			var tempReminder = new ReminderDto
			{
				Id = tempData.Id,
				Title = tempData.Title,
				Text = tempData.Text,
				ReminderTime = tempData.ReminderTime,
				Tags = new List<TagDto>()
			};

			foreach (var tag in tempData.Tags!)
			{
				var tempTag = new TagDto
				{
					Id = tag.Id,
					Name = tag.Name
				};
				tempReminder.Tags!.Add(tempTag);
			}

			return tempReminder;
		}


	}

}
