using FluentValidation;
using UI.Contracts.User;

namespace UI.Validators.User;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(u => u.Login)
            .MinimumLength(5)
            .WithMessage("Login must be at least 5 characters long");

        RuleFor(u => u.Password)
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character from @$!%*?&");

        RuleFor(u => u.Name)
            .MinimumLength(2)
            .WithMessage("Name must be at least 2 characters long")
            .MaximumLength(70)
            .WithMessage("Name cannot exceed 70 characters");

        RuleFor(u => u.Surname)
            .MinimumLength(2)
            .WithMessage("Surname must be at least 2 characters long")
            .MaximumLength(70)
            .WithMessage("Surname cannot exceed 70 characters");

        RuleFor(u => u.Age)
            .InclusiveBetween(18, 120)
            .WithMessage("Age must be between 18 and 120");
    }
}
