using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Identity.Web;
using OGCP.Curriculums.AzureServices.BlobStorages;
using OGCP.Curriculums.Ports;
using OGCP.Curriculums.Reads.ProfileRepository;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class AzureServicesMounter
{
    public static void SetupAzureServices(
        this IServiceCollection Services, IConfiguration Configuration, ILoggingBuilder logging)
    {
        SetupBlobStorage(Services, Configuration);
        SetupADB2C(Services, Configuration);
        SetupApplicationInsights(Services, Configuration, logging);
    }

    public static void SetupBlobStorage(IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddScoped<IBlobProfileImagesServices, BlobProfileImagesServices>();
        Services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(Configuration.GetConnectionString("blobStorage"));
        });
    }

    public static void SetupADB2C(IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

        //var initialScopes = Configuration["AzureAdB2C:Scopes"]?.Split(' ');
        Services.AddAuthorization();
    }

    public static void SetupApplicationInsights(
        IServiceCollection Services, IConfiguration Configuration, ILoggingBuilder loggingBuilder)
    {
        //string appInsightsCS = Configuration["ApplicationInsights:ConnectionString"];
        //loggingBuilder.AddApplicationInsights(
        //        configureTelemetryConfiguration: (config) =>
        //            config.ConnectionString = appInsightsCS,
        //            configureApplicationInsightsLoggerOptions: (options) => { }
        //    );

        //loggingBuilder.AddFilter<ApplicationInsightsLoggerProvider>(null, LogLevel.Trace);

        Services.AddApplicationInsightsTelemetry();
        Services.AddScoped<IApplicationInsights, ApplicationInsightsService>();
    }
}

