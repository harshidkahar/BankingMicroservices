using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Contracts
{
    public record CreateAccounts(int Id, int UserId, decimal Balance);
}
