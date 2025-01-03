using Microsoft.Extensions.Logging.ApplicationInsights;
using OGCP.Curriculum.API.Helpers;

var builder = WebApplication.CreateBuilder(args);
string appInsightsConnStr = builder.Configuration["ApplicationInsights:ConnectionString"];
builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: (config) =>
        config.ConnectionString = appInsightsConnStr,
        configureApplicationInsightsLoggerOptions: (options) =>
        {
            options.IncludeScopes = true;
        }
);

builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("OGCP", LogLevel.Information);

ILogger logger = null;
try
{
    // Create a logger instance
    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
    logger = loggerFactory.CreateLogger("OGCP");

    app.Run();
    logger.LogInformation("MY_TRACKINGS: Start running the application");
}
catch (Exception ex)
{

    logger.LogInformation("MY_TRACKINGS: Exception was catched in exc eption middleware class");
    logger.LogInformation(string.Format("MY_TRACKINGS message: {0}", ex.Message));
    logger.LogInformation(string.Format("MY_TRACKINGS stack trace: {0}", ex.StackTrace));

}
finally
{
}

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.Run();
