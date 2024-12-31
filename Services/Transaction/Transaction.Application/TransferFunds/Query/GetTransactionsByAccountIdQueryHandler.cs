using ErrorOr;
using MediatR;
using Transaction.Application.Interface;
using Transaction.Domain.Entities;

namespace Account.Application.AccountManagement.Query;

public class GetTransactionsByAccountIdQueryHandler : IRequestHandler<GetTransactionsByAccountIdQuery, ErrorOr<List<Transactions>>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByAccountIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ErrorOr<List<Transactions>>> Handle(GetTransactionsByAccountIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetTransactionsByAccountId(query.AccountId);

        return result.Match(
         success => success.ToList(),
         errors => ErrorOr<List<Transactions>>.From(errors)
     );
    }
}

