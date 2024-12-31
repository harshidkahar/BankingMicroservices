using Transaction.Application.Interface;
using ErrorOr;
using MediatR;
using Transaction.Application.Authentication.Commands.Login;

namespace Transaction.Application.Authentication.Commands.TransferFunds;

public class TransferFundsCommandHandler : IRequestHandler<TransferFundsCommand, ErrorOr<TransferFundsResponse>>
{
    private readonly ITransactionService _transactionService;

    public TransferFundsCommandHandler(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    public async Task<ErrorOr<TransferFundsResponse>> Handle(TransferFundsCommand request, CancellationToken cancellationToken)
    {
        var result = await _transactionService.TransferFundsAsync(request.FromAccountId, request.ToAccountId, request.Amount);

        return result.Match(
            success => result,
            errors => ErrorOr<TransferFundsResponse>.From(errors)
        );
    }
}
