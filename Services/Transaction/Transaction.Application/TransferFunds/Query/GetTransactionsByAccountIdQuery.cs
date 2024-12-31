using ErrorOr;
using MediatR;
using Transaction.Domain.Entities;

namespace Account.Application.AccountManagement.Query
{
    public record GetTransactionsByAccountIdQuery(Guid AccountId) : IRequest<ErrorOr<List<Transactions>>>;

}
