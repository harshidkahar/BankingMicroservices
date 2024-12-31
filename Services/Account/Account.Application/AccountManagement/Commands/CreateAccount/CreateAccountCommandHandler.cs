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
using Microsoft.AspNetCore.Http.HttpResults;

namespace Account.Application.AccountManagement.Commands.CreateAccount;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ErrorOr<AccountCreateResponse>>
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ErrorOr<AccountCreateResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _accountRepository.CreateAccountAsync(request.UserId, request.InitialBalance, request.AccountType);

        return result.Match(
             success => new AccountCreateResponse(success),
            errors => ErrorOr<AccountCreateResponse>.From(errors)
        );
    }
}
