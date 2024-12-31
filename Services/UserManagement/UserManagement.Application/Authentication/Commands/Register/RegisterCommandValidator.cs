using UserManagement.Application.Authentication.Commands.Login;
using FluentValidation;

namespace UserManagement.Application.Authentication.Commands.Login
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
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
            RuleFor(x=> x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required")
                .MinimumLength(3)
                .MaximumLength(20);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required")
                .MinimumLength(3)
                .MaximumLength(20);
        }
    }
}
