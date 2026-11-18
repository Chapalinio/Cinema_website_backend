using FluentValidation;
using UI.Contracts.FilmSession;

namespace UI.Validators.FilmSession
{
    public class FilmSessionCreateRequestValidator : AbstractValidator<FilmSessionCreateRequest>
    {
        public FilmSessionCreateRequestValidator()
        {
            RuleFor(fs => fs.HallName)
                .NotEmpty().WithMessage("Hall name is required.")
                .MaximumLength(100).WithMessage("Hall name cannot exceed 100 characters.");

            RuleFor(fs => fs.Price)
                .NotNull().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price cannot be negative.");

            RuleFor(fs => fs.AvailableSeats)
                .NotNull().WithMessage("Available seats count is required.")
                .GreaterThan(0).WithMessage("Available seats count cannot be negative.");

            RuleFor(fs => fs.StartsAt)
                .NotNull().WithMessage("Start time is required");
        }
    }
}
