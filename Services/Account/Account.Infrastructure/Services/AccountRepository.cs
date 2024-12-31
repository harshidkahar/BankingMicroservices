using Utility;
using Account.Application.Contracts;
using Account.Application.Interface;
using Account.Domain.Entities;
using Dapper;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.UnitOfWork;

namespace Account.Infrastructure.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> CreateAccountAsync(Guid userId, decimal initialBalance, string accountType)
        {
            var parameters = new
            {
                UserId = userId,
                Balance = initialBalance, 
                AccountType = accountType
            };
            Accounts? createdAccount = new Accounts();
            try
            {
                createdAccount = _unitOfWork.Connection.Query<Accounts>("[dbo].[CreateAccount]",
                                                  parameters,
                                                  transaction: _unitOfWork.Transaction,
                                                  commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch { return Errors.Account.NotCreated; }

            await _unitOfWork.CommitAsync();
            return createdAccount.AccountId;
        }

        public async Task<ErrorOr<Accounts?>> GetAccountByIdAsync(Guid id)
        {
            var account = _unitOfWork.Connection.QuerySingleOrDefault<Accounts>(
                "SELECT * FROM Accounts WHERE AccountId = @id;",new { id = id }, transaction: _unitOfWork.Transaction);
            if (account is null)
            {
                return Errors.Account.NotFound;
            }
            return account;
        }

        public async Task<ErrorOr<AccountBalance>> UpdateBalanceAsync(Guid accountId, decimal amount)
        {
            var parameters = new
            {
                AccountId = accountId,
                Amount = amount
            };
            var accountBalance = _unitOfWork.Connection.Query<AccountBalance>("[dbo].[UpdateAccount]",
                                                  parameters,
                                                  transaction: _unitOfWork.Transaction,
                                                  commandType: CommandType.StoredProcedure).FirstOrDefault();
            await _unitOfWork.CommitAsync();
            
            if (accountBalance is null)
            {
                return Errors.Account.BalanceUpdateFailed;
            }

            return accountBalance;
        }
    }
}
