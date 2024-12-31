using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.Contracts
{
    public record TransactionDTO
    {
        public Guid AccountId { get; init; }
        public Guid ReferenceId { get; init; }
        public decimal Amount { get; init; }
        public string TransactionType { get; init; }
    }
}
