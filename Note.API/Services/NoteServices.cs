using Note.Application.Notes.Queries.GetNotes;
using Note.API.Entity;

namespace Note.API.Services;

public class NoteServices
{
	public static List<NoteDto> ConvertTempDataToNotes(List<NoteVm> tempData)
	{
		List<NoteDto> notes = new List<NoteDto>();
		foreach (var temp in tempData)
		{
			var temptags = new List<TagDto>();
			foreach (var tag in temp.Tags!)
			{
				var tempTag = new TagDto
				{
					Id = tag.Id,
					Name = tag.Name
				};
				temptags.Add(tempTag);
			}
			var note = new NoteDto
			{
				Id = temp.Id,
				Text = temp.Text,
				Title = temp.Title,
				Tags = new List<TagDto>()
			};
			foreach (var tag in temptags)
			{
				note.Tags!.Add(tag);
			}
			notes.Add(note);
		}
		return notes;
	}
	public static NoteDto ConvertTempDataToNote(NoteVm tempData)
	{
		var temptags = new List<TagDto>();
		foreach (var tag in tempData.Tags!)
		{
			var tempTag = new TagDto
			{
				Id = tag.Id,
				Name = tag.Name
			};
			temptags.Add(tempTag);
		}

		var note = new NoteDto
		{
			Id = tempData.Id,
			Text = tempData.Text,
			Title = tempData.Title,
			Tags = new List<TagDto>()
		};

		foreach (var tag in temptags)
		{
			note.Tags!.Add(tag);
		}

		return note;
	}

}
