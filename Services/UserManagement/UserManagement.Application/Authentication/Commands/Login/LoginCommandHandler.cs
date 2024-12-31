using UserManagement.Application.Interface;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Authentication.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly IAuthenticationService _authService;

    public LoginCommandHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);

        return result.Match(
            success => new AuthenticationResponse(success.Token, success.RefreshToken),
            errors => ErrorOr<AuthenticationResponse>.From(errors)
        );
    }
}
