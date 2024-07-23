using FluentValidation;
using Note.Application.Notes.Commands.CreateTag;

namespace Note.Application.Notes.Commands.CreateNote
{
	public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
	{
		public CreateTagCommandValidator()
		{
			RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required")
			 .MaximumLength(50).WithMessage("Name should not exceed 50 characters");	

		}
	}
}
