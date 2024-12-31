using ErrorOr;
using System.Diagnostics;

namespace Utility
{
    public static class Errors
    {
        public static class Account
        {
            public static Error NotCreated => Error.Validation(
                code: "Account.NotCreated",
                description: "Account not created.");

            public static Error NotFound => Error.Validation(
                code: "Account.NotFound",
                description: "One or both accounts not found.");

            public static Error InsufficientFunds => Error.Validation(
                code: "Account.InsufficientFunds",
                description: "Insufficient funds in the source account.");

            public static Error BalanceUpdateFailed => Error.Validation(
               code: "Account.BalanceUpdateFailed",
               description: "Failed to update balance in the source account.");
            //

            // Transaction.Failed", "Failed to debit source account
        }

        public static class Transaction
        {
            public static Error NotCreated => Error.Validation(
                code: "Transaction.NotCreated",
                description: "Failed to register transaction.");
            public static Error FailedDebit => Error.Validation(
                code: "Transaction.FailedDebit",
                description: "Failed to debit source account.");

            public static Error FailedCredit => Error.Validation(
                code: "Transaction.FailedCredit",
                description: "Failed to credit destination account.");

            public static Error CompensationFailed => Error.Validation(
                code: "Transaction.CompensationFailed",
                description: "Failed to compensate for debit operation.");

            public static Error NotFound => Error.Validation(
                code: "Transaction.NotFound",
                description: "Ther are no transaction for this user.");
        }

        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "The provided credentials are invalid.");

            public static Error UserNotRegistered => Error.Validation(
                code: "Auth.UserNotRegistered",
                description: "User not registered.");

            public static Error UserNotUpdated => Error.Validation(
                code: "Auth.UserNotUpdated",
                description: "User not updated.");

            public static Error UserNotFound => Error.Validation(
                code: "Auth.UserNotFound",
                description: "User not found.");

            public static Error InvalidToken => Error.Validation(
                code: "Auth.InvalidToken",
                description: "Invalid access token or refresh token");
        }
    }
}
