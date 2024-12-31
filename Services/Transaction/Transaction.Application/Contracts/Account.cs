using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.Contracts
{
    public record Account(int Id, int UserId, decimal Balance);

    public record AccountsDTO(  Guid AccountId, 
                                Guid UserId, 
                                string AccountType, 
                                decimal Balance, 
                                bool Status, 
                                DateTime CreatedDate, 
                                DateTime ModifiedDate);
}
