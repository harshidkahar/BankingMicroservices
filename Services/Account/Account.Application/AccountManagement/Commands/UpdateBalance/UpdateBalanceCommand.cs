using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Application.Contracts;
using Account.Domain.Entities;

namespace Account.Application.AccountManagement.Commands.UpdateBalance;

public record UpdateBalanceCommand(Guid AccountId, decimal Amount) : IRequest<ErrorOr<BalanceUpdated>>;

public record BalanceUpdated(decimal UpdatedBalance);
