using Account.Domain.Entities;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.AccountManagement.Query
{
    public record GetAccountQuery(Guid AccountId) : IRequest<ErrorOr<Accounts>>;

}
