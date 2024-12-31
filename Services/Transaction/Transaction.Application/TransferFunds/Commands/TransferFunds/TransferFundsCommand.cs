using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.Authentication.Commands.Login;

public record TransferFundsCommand(Guid FromAccountId, Guid ToAccountId, decimal Amount) : IRequest<ErrorOr<TransferFundsResponse>>;
public class TransferFundsResponse
{
    public Guid SourceAccountId { get; set; }
    public Guid TargetAccountId { get; set; }
    public Guid ReferenceId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public string Status { get; set; }
    public DateTime TransactionDate { get; set; }
}
