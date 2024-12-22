using Microsoft.EntityFrameworkCore.Design;
using OGCP.Curriculum.API.DAL.Mutations.context;

namespace OGCP.Curriculum.API.Helpers.factories;
public class ApplicationWriteDbContextFactory : IDesignTimeDbContextFactory<ApplicationWriteDbContext>
{
    public ApplicationWriteDbContext CreateDbContext(string[] args)
    {
        // Load configuration (e.g., from appsettings.json)
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<ApplicationWriteDbContextFactory>() // Include user secrets if necessary
            .Build();

        // Create the DbProfileWritesContextConfig manually
        var dbProfileConfig = new DbProfileWritesContextConfig
        {
            ConnectionString = configuration.GetConnectionString("conectionDb"),
            UseConsoleLogger = true
        };

        // Return the ApplicationWriteDbContext with the configuration
        return new ApplicationWriteDbContext(dbProfileConfig);
    }
}
