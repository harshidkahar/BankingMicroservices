using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Domain.Entities
{
    public class Transactions
    {
        [Required]
        public Guid TransactionId { get; set; }

        public Guid AccountId { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime Timestamp { get; set; }

        public string ReferenceId { get; set; }

    }
}
