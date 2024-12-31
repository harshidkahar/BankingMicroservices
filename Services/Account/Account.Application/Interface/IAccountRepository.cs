using Account.Application.Contracts;
using Account.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Interface
{
    public interface IAccountRepository
    {
        Task<ErrorOr<Guid>> CreateAccountAsync(Guid userId, decimal initialBalance, string accountType);
        Task<ErrorOr<Accounts?>> GetAccountByIdAsync(Guid id);
        Task<ErrorOr<AccountBalance>> UpdateBalanceAsync(Guid accountId, decimal amount);
    }
}
