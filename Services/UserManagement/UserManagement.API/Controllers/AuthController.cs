using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Authentication.Commands.Login;
//using UserManagement.Application.Authentication.Queries.GetUserDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using UserManagement.Application.Authentication.Queries;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserDetails([FromQuery] GetUserDetailsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.Match(
                success => Ok(success),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }

    }
}
