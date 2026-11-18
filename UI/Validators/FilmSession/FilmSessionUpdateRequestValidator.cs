using FluentValidation;
using UI.Contracts.FilmSession;

namespace UI.Validators.FilmSession
{
    public class FilmSessionUpdateRequestValidator : AbstractValidator<FilmSessionUpdateRequest>
    {
        public FilmSessionUpdateRequestValidator()
        {
            RuleFor(fs => fs.HallName)
                .MaximumLength(100).WithMessage("Hall name cannot exceed 100 characters.");

            RuleFor(fs => fs.Price)
                .GreaterThan(0).WithMessage("Price cannot be negative.");

            RuleFor(fs => fs.AvailableSeats)
                .GreaterThan(0).WithMessage("Available seats count cannot be negative.");
        }
    }
}
