using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.context;

public class ApplicationWriteDbContext : DbContext
{
    private DbProfileWritesContextConfig config;

    public ApplicationWriteDbContext(DbProfileWritesContextConfig config)
    {
        this.config = config;
    }

    //This constructor is only for testing porpouses
    public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> dbContext)
        : base(dbContext) { }

    public DbSet<QualifiedProfile> QualifiedProfiles { get; set; }
    public DbSet<GeneralProfile> GeneralProfiles { get; set; }
    public DbSet<StudentProfile> StudentProfiles { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<DegreeEducation> DegreeEducations { get; set; }
    public DbSet<ResearchEducation> ResearchEducations { get; set; }
    public virtual DbSet<Dictionary<string, object>> Certifications =>
            Set<Dictionary<string, object>>("Certification");

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<EducationLevel>()
            .HaveConversion<EducationLevelConverter>();

        base.ConfigureConventions(configurationBuilder);
    }

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
                )
                .UseLazyLoadingProxies();

            //.UseLazyLoadingProxies();//To enable lazy loading (Only writes, never reads)

            if (config.UseConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();//Now we can see the sql query on the console for performance purposes
            }
        }
    }

    //DbContext maintain a snapshot for any new migration, compares with the previous ones
    //and knows exactly what to update-run for the next migration to database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        this.AddPropertyBags(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationWriteDbContext).Assembly,
            WriteConfigurationsFilter);
        
        base.OnModelCreating(modelBuilder);
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("context.Configurations") ?? false;

    private void AddPropertyBags(ModelBuilder modelBuilder)
    {
        //We do not need to map this to an CLR class
        //Easy to add metadata without modifying database schema
        //We can add attributes in a flexible way
        //This is called indexer properties and allow us to create property bags.
        //In a many to many relationship we create a property bag, witout creating the intermediate join class.
        //We could create a json object in settings.json to setup this property bag based on the json key/values
        modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
            "Certifications",
            entity =>
            {
                entity.Property<int>("Id");
                entity.Property<int>("ProfileId");
                entity.Property<string>("CertificationName");
                entity.Property<string>("IssuingOrganization");
                entity.Property<DateTime>("DateIssued");
                entity.Property<DateTime?>("ExpirationDate");
                entity.Property<string>("Description");
            });


        //In a system where you synchronize user profiles between databases, you can compare the checksum values to quickly identify records that need updates.
        //You can validate the integrity of user data during migrations or imports by ensuring that the checksum matches the recalculated value based on the migrated columns.
        //Auditing: In scenarios where you need to detect unauthorized or accidental changes to critical fields, the checksum can serve as an additional layer of protection.
        if (this.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer")
        {
            modelBuilder.Entity<Language>()
                .Property<byte[]>("Checksum")
                .HasComputedColumnSql("CONVERT(VARBINARY(1024), CHECKSUM([Name], [Level]))");
        }
        else
        {
            // SQLite doesn't support CHECKSUM, use a placeholder or omit the computed column for SQLite
            modelBuilder.Entity<Language>()
                .Property<byte[]>("Checksum")
                .HasComputedColumnSql(null);
        }
    }
}
