using ErrorOr;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.Authentication.Commands.Login;
using Transaction.Application.Contracts;
using Transaction.Application.Interface;
using Transaction.Domain.Entities;
using Utility;

namespace Transaction.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IAccountService accountService, ITransactionRepository transactionRepository)
        {
            _accountService = accountService;
            _transactionRepository = transactionRepository;
        }

        public async Task<ErrorOr<TransferFundsResponse>> TransferFundsAsync(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccount = await _accountService.GetAccountAsync(fromAccountId);
            var toAccount = await _accountService.GetAccountAsync(toAccountId);

            if (fromAccount is null || toAccount is null)
                return Errors.Account.NotFound;

            if (fromAccount.Balance < amount)
                return Errors.Account.InsufficientFunds;

            var debitSuccess = await _accountService.UpdateAccountBalanceAsync(fromAccountId, -amount);
            if (!debitSuccess)
                return Errors.Transaction.FailedDebit;


            var creditSuccess = await _accountService.UpdateAccountBalanceAsync(toAccountId, amount);
            if (!creditSuccess)
            {
                var compensationSuccess = await _accountService.UpdateAccountBalanceAsync(fromAccountId, amount);
                if (!compensationSuccess)
                {
                    return Error.Failure("Transaction.CompensationFailed", "Failed to compensate for debit operation.");
                }
                return Errors.Transaction.FailedCredit;
            }

            // Generate a reference ID for the transaction
            Guid referenceId = Guid.NewGuid();

            var debitTransaction = new TransactionDTO
            {
                AccountId = fromAccountId,
                Amount = -amount,
                TransactionType = "Debit",
                ReferenceId = referenceId
            };

            var creditTransaction = new TransactionDTO
            {
                AccountId = toAccountId,
                Amount = amount,
                TransactionType = "Credit",
                ReferenceId = referenceId
            };

            var debitTransactionDetails = await _transactionRepository.RegisterTransactionAsync(debitTransaction);

            await _transactionRepository.RegisterTransactionAsync(creditTransaction);


            return new TransferFundsResponse
            {
                SourceAccountId = fromAccountId,
                TargetAccountId = toAccountId,
                ReferenceId = referenceId,
                Amount = amount,
                TransactionType = "Debit", // Or "Credit" based on the type
                Status = "Transaction Successful", // Corrected the spelling of "Successful"
                TransactionDate = DateTime.UtcNow // Add the timestamp for the transaction
            };

        }
    }
}
