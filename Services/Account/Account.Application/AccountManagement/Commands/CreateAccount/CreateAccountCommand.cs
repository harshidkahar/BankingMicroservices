using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Application.Contracts;
using Account.Domain.Entities;

namespace Account.Application.AccountManagement.Commands.CreateAccount;

public record CreateAccountCommand(Guid UserId, decimal InitialBalance, string AccountType) : IRequest<ErrorOr<AccountCreateResponse>>;

public record AccountCreateResponse(Guid AccountId);