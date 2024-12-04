using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.domainmodel;
namespace OGCP.Curriculum.API.DAL.Queries.context;
public class DbReadProfileContext : DbContext
{
    private DbProfileReadsContextConfig config;

    public DbReadProfileContext(DbProfileReadsContextConfig config)
    {
        this.config = config;
    }

    public DbSet<ProfileReadModel> Profiles { get; set; }
    public DbSet<LanguageReadModel> Languages { get; set; }
    public DbSet<EducationReadModel> Educations { get; set; }
    public virtual DbSet<Dictionary<string, object>> Certifications =>
            Set<Dictionary<string, object>>("Certification");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //This is already tested in unit test scenarios
        //So just configure for sql server
        if (!optionsBuilder.IsConfigured)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(config.ConnectionString,
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,         // Maximum number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(15), // Max delay between retries
                        errorNumbersToAdd: null
                    )
                ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (config.UseConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();//Now we can see the sql query on the console for performance purposes
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DbReadProfileContext).Assembly,
            WriteConfigurationsFilter);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
    type.FullName?.Contains("Queries.context") ?? false;
}
