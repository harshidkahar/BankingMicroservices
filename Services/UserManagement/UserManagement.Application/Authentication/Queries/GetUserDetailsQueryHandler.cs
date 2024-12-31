using ErrorOr;
using MediatR;

namespace UserManagement.Application.Authentication.Queries;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, ErrorOr<UserDetailsResponse>>
{
   // private readonly IUnitOfWork _unitOfWork;

    public GetUserDetailsQueryHandler()//IUnitOfWork unitOfWork)
    {
        //_unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UserDetailsResponse>> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
    {
        // Query the database for the user
        //var user = await _unitOfWork.Connection.QuerySingleOrDefaultAsync<User>(
        //    "SELECT * FROM Users WHERE Email = @Email",
        //    new { Email = query.Email },
        //    _unitOfWork.Transaction);

        //// Return an error if the user is not found
        //if (user is null)
        //{
        //    return Errors.Authentication.UserNotFound;
        //}

        // Map the User entity to UserDetailsResponse
        var response = new UserDetailsResponse(
            //user.Id,
            //user.FirstName,
            //user.LastName,
            //user.Email,
            //user.CreatedAt);
            Guid.NewGuid(),
            "Harshid",
            "Kahar",
            "harshidkahar@gmail.com",
            DateTime.UtcNow);

        return response;
    }
}

