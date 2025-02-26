using Serilog;
using UserManagement.Application.Common.Interface;


namespace UserManagement.Infrastructure.Logging;

public class LoggerManager : ILoggerService
{
    private readonly ILogger _logger;

    public LoggerManager()
    {
        _logger = Log.Logger;
    }

    public void LogInformation(string message) => _logger.Information(message);
    public void LogWarning(string message) => _logger.Warning(message);
    public void LogError(string message) => _logger.Error(message);
    public void LogDebug(string message) => _logger.Debug(message);
}