using Dapper;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts;
using UserManagement.Application.Interface;
using Utility;
using UserManagement.Domain.Entities;
using System.Data;
using System.Data.Common;
using Utility.UnitOfWork;

namespace UserManagement.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> AuthenticateAsync(string email, string password)
    {
        try
        {
            var parameters = new
            {
                Email = email,
                Password = HashingHelper.ComputeSHA512Hash(password)
            };
            User? user = new User();
            try
            {
                user = _unitOfWork.Connection.Query<User>("[dbo].[UserLogin]",
                                                  parameters,
                                                  transaction: _unitOfWork.Transaction,
                                                  commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch { return Errors.Authentication.InvalidCredentials; }

            if (user == null)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            var token = _jwtTokenGenerator.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            var updateParameters = new
            {
                Id = user.Id,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
            try
            {
                var userData = await _unitOfWork.Connection.QueryFirstOrDefaultAsync("[dbo].[UserUpdate]",
                                                            updateParameters,
                                                            transaction: _unitOfWork.Transaction, 
                                                            commandType: CommandType.StoredProcedure);
               
                if (userData == null)
                {
                    return Errors.Authentication.UserNotUpdated;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception if necessary
                // Return a relevant error if the operation failed
                return Errors.Authentication.UserNotUpdated; // Example: DatabaseError can represent any DB operation error
            }

            await _unitOfWork.CommitAsync();

            return new AuthenticationResult(token, refreshToken, user.Email);
        }
        catch (Exception ex)
        {
            // Log or handle the exception if necessary
            // Return a relevant error if the operation failed
            return Errors.Authentication.InvalidCredentials; // Example: DatabaseError can represent any DB operation error
        }
    }

    private bool VerifyPassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);

    private string GenerateRefreshToken() =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));


    public async Task<ErrorOr<Guid>> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        await _unitOfWork.Connection.QuerySingleOrDefaultAsync<User>(
            "INSERT INTO Users (FirstName, LastName, Email, Password, CreatedOn, Status) Values (@FirstName, @LastName, @Email, @Password,@CreatedOn, @Status)", new { Email = email, Password = HashingHelper.ComputeSHA512Hash(password), FirstName =firstName, LastName = lastName, CreatedOn = DateTime.UtcNow, Status = 1 }, _unitOfWork.Transaction);
        await _unitOfWork.CommitAsync();

        var user = _unitOfWork.Connection.QueryFirstOrDefault<User>(
            "select top(1) Id from Users order by Id desc");
        if (user is null)
        {
            return Errors.Authentication.UserNotRegistered;
        }
        return user.Id;
    }
    public async Task<ErrorOr<User?>> GetUserByIdAsync(Guid id)
    {
        var user = _unitOfWork.Connection.QueryFirstOrDefault<User>(
            "SELECT * FROM Users WHERE Id = @Id;",new {Id = id });
        if (user is null)
        {
            return Errors.Authentication.UserNotFound;
        }
        return user;
    }
}
