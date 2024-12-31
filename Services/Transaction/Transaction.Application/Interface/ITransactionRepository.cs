using ErrorOr;
using Transaction.Application.Contracts;
using Transaction.Domain.Entities;

namespace Transaction.Application.Interface
{
    public interface ITransactionRepository
    {
        Task<ErrorOr<Transactions>> RegisterTransactionAsync(TransactionDTO transaction);
        Task<ErrorOr<List<Transactions>>> GetTransactionsByAccountId(Guid accountId);
    }
}
