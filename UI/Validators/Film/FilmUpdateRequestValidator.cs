using FluentValidation;
using UI.Contracts.Film;

namespace UI.Validators.Film;

public class FilmUpdateRequestValidator : AbstractValidator<FilmUpdateRequest>
{
    public FilmUpdateRequestValidator()
    {
        RuleFor(f => f.Title)
            .MinimumLength(1)
            .WithMessage("Title must be at least 1 character long")
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters.");

        RuleFor(f => f.Description)
            .MinimumLength(1)
            .WithMessage("Description must be at least 1 character long")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(f => f.PosterUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("PosterUrl must be a valid URL.");

        RuleFor(f => f.FilmUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .WithMessage("FilmUrl must be a valid URL.");

        RuleFor(f => f.ReleaseYear)
            .InclusiveBetween(1888, 2027)
            .WithMessage("ReleaseYear must be between 1888 and 2027.");

        RuleFor(f => f.LengthInMinutes)
            .InclusiveBetween(40, 500)
            .WithMessage("LengthInMinutes must be between 40 and 500.");
    }
}
