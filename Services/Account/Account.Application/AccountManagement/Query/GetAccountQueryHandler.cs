using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Application.Interface;
using Account.Domain.Entities;
using ErrorOr;
using MediatR;

namespace Account.Application.AccountManagement.Query;

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, ErrorOr<Accounts>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<Accounts>> Handle(GetAccountQuery query, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.GetAccountByIdAsync(query.AccountId);

        return result.Match(
            success => result,
            errors => ErrorOr<Accounts>.From(errors)
        );
    }

}

