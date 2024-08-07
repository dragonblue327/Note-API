﻿using FluentValidation;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
	{
		public CreateNoteCommandValidator()
		{
			RuleFor(a => a.Title).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(50).WithMessage("Name should not exceed 50 characters").MinimumLength(3).WithMessage("Name should be more than 3 characters");

			RuleFor(a => a.Text).NotEmpty().WithMessage("Text is required")
				.MaximumLength(200).WithMessage("Text should not exceed 200 characters").MinimumLength(3).WithMessage("Text should be more than 3 characters");

		}
	}
}
