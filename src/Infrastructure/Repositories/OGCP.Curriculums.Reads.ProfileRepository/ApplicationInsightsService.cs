using Microsoft.Extensions.Logging;
using OGCP.Curriculums.Ports;

namespace OGCP.Curriculums.Reads.ProfileRepository;

public class ApplicationInsightsService : IApplicationInsights
{
    private readonly ILogger<ApplicationInsightsService> _logger;

    public ApplicationInsightsService(ILogger<ApplicationInsightsService> logger)
    {
        _logger = logger;
    }

    // Logs a simple informational message
    public void ApplicationInsightsLogger()
    {
        _logger.LogInformation("ApplicationInsightsLogger Method was called.");
    }

    // Logs an informational message
    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    // Logs a warning message
    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    // Logs an error message
    public void LogError(string message)
    {
        _logger.LogError(message);
    }

    // Logs an exception with an optional custom message
    public void LogException(Exception exception, string customMessage = null)
    {
        if (string.IsNullOrEmpty(customMessage))
        {
            _logger.LogError(exception, "An exception occurred.");
        }
        else
        {
            _logger.LogError(exception, customMessage);
        }
    }
}
