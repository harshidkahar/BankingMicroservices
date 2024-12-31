using Account.Application.AccountManagement.Commands.CreateAccount;
using Account.Application.AccountManagement.Commands.UpdateBalance;
using Account.Application.AccountManagement.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class Accounts : ApiController
    {
        private readonly IMediator _mediator;

        public Accounts(IMediator mediator)
        { 
            _mediator = mediator;
        }
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }

        [HttpGet("GetAccount")]
        public async Task<IActionResult> GetAccount(GetAccountQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }

        [HttpPatch("UpdateBalance")]
        public async Task<IActionResult> UpdateBalance(UpdateBalanceCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }
    }
}
