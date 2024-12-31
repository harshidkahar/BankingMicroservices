using ErrorOr;

namespace UserManagement.Common;
public static class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "The provided credentials are invalid.");
    }
}
