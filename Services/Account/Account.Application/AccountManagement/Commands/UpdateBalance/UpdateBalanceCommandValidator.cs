using FluentValidation;

namespace Account.Application.AccountManagement.Commands.UpdateBalance;
public class UpdateBalanceCommandValidator : AbstractValidator<UpdateBalanceCommand>
{
    public UpdateBalanceCommandValidator()
    {
        RuleFor(x => x.AccountId)
          .NotNull()
          .WithMessage("Account ID is required.");
    }
}

