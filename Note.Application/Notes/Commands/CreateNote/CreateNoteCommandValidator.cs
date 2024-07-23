using FluentValidation;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
	{
		public CreateNoteCommandValidator()
		{
			RuleFor(a => a.Title).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(200).WithMessage("Name should not exceed 200 characters");

			RuleFor(a => a.Text).NotEmpty().WithMessage("Text is required");

		}
	}
}
