using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Contracts;

public class AuthenticationResult
{
    public string Token { get; }
    public string RefreshToken { get; }
    public string UserId { get; }

    public AuthenticationResult(string token, string refreshToken, string userId)
    {
        Token = token;
        RefreshToken = refreshToken;
        UserId = userId;
    }
}

