using Account.Application.AccountManagement.Commands.CreateAccount;
using FluentValidation;

namespace Account.Application.AccountManagement.Commands.CreateAccount;
public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
          .NotNull()
          .WithMessage("User ID is required.");
        RuleFor(x => x.InitialBalance)
            .NotEmpty()
            .WithMessage("Intial Balance is required");
    }
}

