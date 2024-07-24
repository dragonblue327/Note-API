using FluentValidation;

namespace Note.Application.Notes.Commands.CreateReminder
{
	public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
	{
		public CreateReminderCommandValidator()
		{
			RuleFor(a => a.Title).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(200).WithMessage("Name should not exceed 200 characters");

			RuleFor(a => a.Text).NotEmpty().WithMessage("Text is required");

			RuleFor(a => a.ReminderTime).NotEmpty().WithMessage("Reminder Time is required");
			RuleFor(a => a.Tags).NotEmpty().WithMessage("You cannot creat reminder without atag");

		}
	}
}
