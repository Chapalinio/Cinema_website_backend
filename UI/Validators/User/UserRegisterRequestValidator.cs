using FluentValidation;
using UI.Contracts.User;

namespace UI.Validators.User;

public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
{
    public UserRegisterRequestValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("User name is required")
            .MinimumLength(3).WithMessage("User name must be at least 3 characters long")
            .MaximumLength(30).WithMessage("User name cannot exceed 30 characters");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(256).WithMessage("Email cannot exceed 256 characters");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character from @$!%*?&");

        RuleFor(u => u.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Equal(u => u.Password).WithMessage("Confirm password does not match password");

        RuleFor(u => u.City)
            .NotEmpty().WithMessage("City is required");

        RuleFor(u => u.FavouriteGenre)
            .NotEmpty().WithMessage("Favourite genre is required");

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
            .MaximumLength(70).WithMessage("Name cannot exceed 70 characters");

        RuleFor(u => u.Surname)
            .NotEmpty().WithMessage("Surname is required")
            .MinimumLength(2).WithMessage("Surname must be at least 2 characters long")
            .MaximumLength(70).WithMessage("Surname cannot exceed 70 characters");

        RuleFor(u => u.Age)
            .InclusiveBetween(18, 120).WithMessage("Age must be between 18 and 120");
    }
}
