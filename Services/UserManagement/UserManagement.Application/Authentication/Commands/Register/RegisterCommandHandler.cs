using UserManagement.Application.Interface;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Authentication.Commands.Login;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Guid>>
{
    private readonly IAuthenticationService _authService;

    public RegisterCommandHandler(IAuthenticationService authService)
    {
        _authService = authService;
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request.FirstName,request.LastName,request.Email, request.Password);

        return result.Match(
            success => result,
            errors => ErrorOr<Guid>.From(errors)
        );
    }
}
