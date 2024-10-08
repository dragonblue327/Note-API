﻿using FluentValidation;

namespace Note.Application.Notes.Commands.CreateReminder
{
	public class CreateReminderCommandValidator : AbstractValidator<CreateReminderCommand>
	{
		public CreateReminderCommandValidator()
		{
			RuleFor(a => a.Title).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(200).WithMessage("Name should not exceed 200 characters");
			RuleFor(a => a.Text).NotEmpty().WithMessage("Text is required");
			RuleFor(a => a.ReminderTime)
			.NotEmpty().WithMessage("Reminder Time is required")
			.Must(BeAValidTime).WithMessage("Reminder Time cannot be in the past");

		}
		private bool BeAValidTime(DateTime reminderTime)
		{
			return reminderTime >= DateTime.Now.AddHours(-1);
		}
	}
}
