using Microsoft.EntityFrameworkCore.Design;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Queries.context;

namespace OGCP.Curriculum.API.Helpers.factories;
public class ApplicationReadDbContextFactory : IDesignTimeDbContextFactory<ApplicationReadDbContext>
{
    public ApplicationReadDbContext CreateDbContext(string[] args)
    {
        // Load configuration (e.g., from appsettings.json)
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<ApplicationReadDbContextFactory>() // Include user secrets if necessary
            .Build();

        // Create the DbProfileWritesContextConfig manually
        var dbProfileConfig = new DbProfileReadsContextConfig
        {
            ConnectionString = configuration.GetConnectionString("conectionDb"),
            UseConsoleLogger = true
        };

        // Return the ApplicationWriteDbContext with the configuration
        return new ApplicationReadDbContext(dbProfileConfig);
    }
}