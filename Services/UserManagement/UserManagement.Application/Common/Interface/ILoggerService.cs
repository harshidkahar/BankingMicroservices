using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Common.Interface;

public interface ILoggerService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogDebug(string message);
}
