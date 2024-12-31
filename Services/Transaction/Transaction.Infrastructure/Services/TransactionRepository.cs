using Dapper;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Application.Contracts;
using Transaction.Application.Interface;
using Transaction.Domain.Entities;
using Utility;
using Utility.UnitOfWork;

namespace Transaction.Infrastructure.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Transactions>>> GetTransactionsByAccountId(Guid accountId)
        {
            var transactions = _unitOfWork.Connection.Query<Transactions>(
                "SELECT * FROM Transactions WHERE AccountId = @id;", new { id = accountId }, transaction: _unitOfWork.Transaction);
            if (transactions is null)
            {
                return Errors.Transaction.NotFound;
            }
            return transactions.ToList();
        }


        public async Task<ErrorOr<Transactions>> RegisterTransactionAsync(TransactionDTO transactionDto)
        {
            Transactions? _transaction = new Transactions();
            try
            {
                try
                {
                    _transaction = _unitOfWork.Connection.Query<Transactions>("[dbo].[RegisterTransactions]",
                                                      transactionDto,
                                                      transaction: _unitOfWork.Transaction,
                                                      commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                catch { return Errors.Transaction.NotCreated; }

                await _unitOfWork.CommitAsync();
            }
            catch { }
            return _transaction;
        }
    }
}
