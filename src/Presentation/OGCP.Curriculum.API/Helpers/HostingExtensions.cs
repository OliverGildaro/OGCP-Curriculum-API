using OGCP.Curriculum.API.Helpers.DIMounters;

namespace OGCP.Curriculum.API.Helpers;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        try
        {
            builder.Services.SetupControllers();
            builder.Services.SetupCommands();
            builder.Services.SetupQueries();
            builder.Services.SetupServices();
            builder.Services.SetupAzureServices(builder.Configuration, builder.Logging);
            builder.Services.SetupRepositories(builder.Configuration);
            builder.Services.SetupDbContext(builder.Configuration);
            return builder.Build();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        // Configure the HttP request pipeline.
        //the request fgoes from one midleware to the next one in the pipeline
        app.UseHttpsRedirection();

        app.UseCors("AllowSpecificOrigins");

        //In the case that the user fails authorization, from this midleware 403 is returned
        //app.UseAuthorization();

        app.UseRequestLocalization(options =>
        {
            string[] supportedCultures = { "en", "en-US", "es", "fr-FR" };

            options.SetDefaultCulture("en-US");

            options.AddSupportedCultures(supportedCultures);
            options.AddSupportedUICultures(supportedCultures);
            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        app.MapControllers();

        return app;
    }
}
