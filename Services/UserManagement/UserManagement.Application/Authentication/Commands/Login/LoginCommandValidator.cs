using UserManagement.Application.Authentication.Commands.Login;
using FluentValidation;

namespace UserManagement.Application.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() 
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Email is required.")
              .MinimumLength(2)
              .MaximumLength(100);
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .MaximumLength(10);
        }
    }
}
