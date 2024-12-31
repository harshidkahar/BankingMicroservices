using ErrorOr;
using Transaction.Application.Authentication.Commands.Login;

namespace Transaction.Application.Interface
{
    public interface ITransactionService
    {
        Task<ErrorOr<TransferFundsResponse>> TransferFundsAsync(Guid fromAccountId, Guid toAccountId, decimal amount);
    }
}
