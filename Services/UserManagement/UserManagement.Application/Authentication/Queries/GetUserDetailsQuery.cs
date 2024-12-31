using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Authentication.Queries
{
    public record GetUserDetailsQuery(Guid Id) : IRequest<ErrorOr<UserDetailsResponse>>;

    public record UserDetailsResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt);

}
