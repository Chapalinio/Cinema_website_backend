using FluentValidation;
using UI.Contracts.Film;

namespace UI.Validators.Film;

public class FilmCreateRequestValidator : AbstractValidator<FilmCreateRequest>
{
    public FilmCreateRequestValidator()
    {
        RuleFor(f => f.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MinimumLength(1)
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters.");

        RuleFor(f => f.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(1)
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");
    
        RuleFor(f => f.PosterUrl)
            .NotEmpty()
            .WithMessage("PosterUrl is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("PosterUrl must be a valid URL.");

        RuleFor(f => f.FilmUrl)
            .NotEmpty()
            .WithMessage("FilmUrl is required.")
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("FilmUrl must be a valid URL.");

        RuleFor(f => f.ReleaseYear)
            .NotNull()
            .WithMessage("ReleaseYear is required.")
            .InclusiveBetween(1888, 2027)
            .WithMessage("ReleaseYear must be between 1888 and 2027.");

        RuleFor(f => f.LengthInMinutes)
            .NotNull()
            .WithMessage("LengthInMinutes is required.")
            .InclusiveBetween(40, 500)
            .WithMessage("LengthInMinutes must be between 40 and 500.");
    }
}
