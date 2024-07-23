using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
	{
		public CreateNoteCommandValidator()
		{
			RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(200).WithMessage("Name should not exceed 200 characters");

			RuleFor(a => a.Text).NotEmpty().WithMessage("Text is required");

		}
	}
}
