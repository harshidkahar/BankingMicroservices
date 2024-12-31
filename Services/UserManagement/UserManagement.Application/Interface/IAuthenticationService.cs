using UserManagement.Application.Contracts;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interface;

public interface IAuthenticationService
{
    Task<ErrorOr<AuthenticationResult>> AuthenticateAsync(string email, string password);
    Task<ErrorOr<Guid>> RegisterAsync(string firstName, string lastName, string email, string password);
    Task<ErrorOr<User?>> GetUserByIdAsync(Guid id);
}
