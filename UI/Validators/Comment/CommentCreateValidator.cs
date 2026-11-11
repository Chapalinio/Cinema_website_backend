using UI.Contracts.Comment;
using FluentValidation;

namespace UI.Validators.Comment;

public class CommentCreateValidator : AbstractValidator<CommentCreateRequest>
{
    public CommentCreateValidator()
    {
        RuleFor(c => c.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(500).WithMessage("Text cannot exceed 500 characters.");

        RuleFor(c => c.AuthorName)
            .NotEmpty().WithMessage("Author name is required.")
            .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters.");

        RuleFor(c => c.FilmId)
            .NotNull().WithMessage("Film ID is required.")
            .GreaterThan(0).WithMessage("Film ID must be greater than 0.");
    }
}