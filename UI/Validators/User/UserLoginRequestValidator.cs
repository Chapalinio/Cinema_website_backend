using FluentValidation;
using UI.Contracts.User;

namespace UI.Validators.User;

public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(u => u.Login)
            .NotEmpty()
            .WithMessage("Login is required");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}
