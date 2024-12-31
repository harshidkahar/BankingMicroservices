using Transaction.Application.Authentication.Commands.Login;
using FluentValidation;

namespace Transaction.Application.Authentication.Commands.TransferFunds
{
    public class TransferFundsCommandValidator : AbstractValidator<TransferFundsCommand>
    {
        public TransferFundsCommandValidator() 
        {
            RuleFor(x => x.Amount)
              .GreaterThan(0)
              .WithMessage("Amount should be freater than 0.");
            RuleFor(x => x.FromAccountId)
                .NotEmpty()
                .WithMessage("From Account Id is required");
            RuleFor(x => x.ToAccountId)
                .NotEmpty()
                .WithMessage("To Account Id is required");
        }
    }
}
