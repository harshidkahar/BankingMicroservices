using UserManagement.Application.Interface;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interface;
using Microsoft.Extensions.Logging;

namespace UserManagement.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IAuthenticationService _authService;
    private readonly ILoggerService _logger;
    public LoginCommandHandler(IAuthenticationService authService, ILoggerService logger)
    {
        _authService = authService;
        _logger = logger;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);
        _logger.LogInformation($"User {request.Email} attempted to log in.");
        return result.Match(
            success => new AuthenticationResponse(success.Token, success.RefreshToken),
            errors => ErrorOr<AuthenticationResponse>.From(errors)
        );
    }
}
