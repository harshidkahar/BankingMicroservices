using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Transaction.Application.Contracts;
using Transaction.Application.Interface;
using static Utility.Errors;

namespace Transaction.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountService> _logger;

        public AccountService(HttpClient httpClient, ILogger<AccountService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AccountsDTO?> GetAccountAsync(Guid accountId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "accounts/GetAccount");
                var payload = new
                {
                    accountId = accountId
                };
                var content = new StringContent(
                    JsonSerializer.Serialize(payload), // Use JsonSerializer for robust JSON serialization
                    Encoding.UTF8,
                    "application/json"
                );
                request.Content = content;
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var account = await response.Content.ReadFromJsonAsync<AccountsDTO>();

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve account {AccountId}", accountId);
                return null;
            }
        }

        public async Task<bool> UpdateAccountBalanceAsync(Guid accountId, decimal amount)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Patch, _httpClient.BaseAddress + "accounts/UpdateBalance");
                // Create the JSON payload
                var payload = new
                {
                    accountId = accountId,
                    amount = amount
                };
                var content = new StringContent(
                    JsonSerializer.Serialize(payload), // Use JsonSerializer for robust JSON serialization
                    Encoding.UTF8,
                    "application/json"
                );
                request.Content = content;
                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update balance for account {AccountId}", accountId);
                return false;
            }
        }

    }
}
