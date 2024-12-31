using Account.Application.AccountManagement.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application.Authentication.Commands.Login;
using Transaction.Application.Authentication.Commands.TransferFunds;
using Transaction.Application.Interface;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class Transactions : ApiController
    {
        private readonly IMediator _mediator;

        public Transactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("TransferFunds")]
        public async Task<IActionResult> TransferFunds([FromBody] TransferFundsCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }


        [HttpGet("getTransactionsByAccountId")]
        public async Task<IActionResult> GetTransactionsByAccountId([FromQuery] GetTransactionsByAccountIdQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }
    }
}
