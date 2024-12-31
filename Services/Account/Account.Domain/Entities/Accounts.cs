using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
    public class Accounts
    {
        public Guid AccountId { get; set; } = Guid.NewGuid();
        public Guid UserId{ get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
