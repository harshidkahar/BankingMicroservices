using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Authentication.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

public record AuthenticationResponse(string Token, string RefreshToken);
