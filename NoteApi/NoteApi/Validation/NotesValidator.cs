using FluentValidation;
using Note.BLL.DTO;

namespace NoteApi.Validation;

public class NotesValidator: AbstractValidator<NoteRequest>
{
    public NotesValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
            .NotNull().WithMessage("Title is required")
            .MaximumLength(50).WithMessage("Title length must be between 3 and 50 characters")
            .MinimumLength(3).WithMessage("Title length must be between 3 and 50 characters");
        RuleFor(x=>x.Description).NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(50).WithMessage("Description length must be between 10 and 50 characters")
            .MinimumLength(10).WithMessage("Description length must be between 10 and 50 characters");
    }
}