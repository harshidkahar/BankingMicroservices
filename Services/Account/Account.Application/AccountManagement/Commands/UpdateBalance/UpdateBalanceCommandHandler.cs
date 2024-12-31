using Account.Application.Contracts;
using Account.Application.Interface;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Application.Contracts;
using Account.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Account.Application.AccountManagement.Commands.UpdateBalance;

public class UpdateBalanceCommandHandler : IRequestHandler<UpdateBalanceCommand, ErrorOr<BalanceUpdated>>
{
    private readonly IAccountRepository _accountRepository;

    public UpdateBalanceCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<BalanceUpdated>> Handle(UpdateBalanceCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.UpdateBalanceAsync(request.AccountId, request.Amount);

        return result.Match(
            success => new BalanceUpdated(success.Balance),
            errors => ErrorOr<BalanceUpdated>.From(errors)
        );

    }
}
