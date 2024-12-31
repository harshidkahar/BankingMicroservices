using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.Contracts;

namespace Transaction.Application.Interface
{
    public interface IAccountService
    {
        Task<AccountsDTO?> GetAccountAsync(Guid accountId);
        Task<bool> UpdateAccountBalanceAsync(Guid accountId, decimal amount);
    }
}
