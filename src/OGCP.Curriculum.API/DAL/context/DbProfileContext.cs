using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.utils;
namespace OGCP.Curriculum.API.repositories
{
    //1. The DbContext affects how EF infers the db schema
    //2. First EF create the sql query, then open a connection to database, executed the query
    //Receives tabular objects, materialize results as objects and add tracking details to dbcontext
    //3. Queries are composable
    //4. Find method is an ef method not a linq method and executes inmediately from database
    //5. OrderBy().ThenBy()
    //6. Take().Skip()
    //MEthods
    // 7. LinQ to entities agregate execution methods
    //Estos métodos calculan un valor único basado en los datos de la consulta.
    //First() return the first element and throw and exception if not found it
    //FirstOrdefault() reeturn the first match or null
    //Count(), Sum(), min(), max(), average()
    //Last and LastOrDefault will only work if you use OrderBy()
    // 8. No aggregation methods
    //ToList()
    //9. DbSet.AsTracking() or DbSet.AsNoTracking()
    //We can setup also in the DBContext what kind of tracking we want to do onCOnfiguring
    //The dbcontext session begins add the moment we execute a query or save data
    //The EntityEntry object tracks the state of an object, the dbcontext contains this ENtityEntry
    //Entity is an in-memory object that the dbcontext tracker is aware
    //TRACKING AND SAVIND DATA FLOW
    //1. EF core create tracking object for each entity
    //2. DBContext maintain entityEntries
    //3. OnSaveChanges
    //The dbcontext reads the current and original values from the EntityEntry and with a help of the provider determined the sql command
    //4. Return # affected and keys
    //5 updates dbcontext entities with pk and fk
    //6. Reset entity state info
    public class DbProfileContext : DbContext
    {
        //ValueComparer code helps Entity Framework Core track changes within string[]
        //Maybe value objects may help tp dp this calculation
        private ValueComparer<string[]> stringArrayComparer = new ValueComparer<string[]>(
            (c1, c2) => c1.SequenceEqual(c2),                // Compare two arrays for equality
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),  // Generate a hash code for the array
            c => c.ToArray());                               // Create a snapshot of the array
        private DbProfileContextConfig config;

        public DbProfileContext(DbProfileContextConfig config)
        {
            this.config = config;
        }

        //This constructor is only for testing porpouses
        public DbProfileContext(DbContextOptions<DbProfileContext> dbContext)
            : base(dbContext)
        {

        }

        public DbSet<QualifiedProfile> QualifiedProfiles { get; set; }
        public DbSet<GeneralProfile> GeneralProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<Language> Languages { get; set; }
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
                    .UseSqlServer(this.config.ConnectionString,
                        sqlOptions => sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 3,         // Maximum number of retry attempts
                            maxRetryDelay: TimeSpan.FromSeconds(15), // Max delay between retries
                            errorNumbersToAdd: null
                        )
                    );
                //.UseLazyLoadingProxies();//To enable lazy loading (Only writes, never reads)

                if (this.config.UseConsoleLogger)
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
            this.AddPRofileModel(modelBuilder);
            this.AddLanguageModel(modelBuilder);
            this.AddJobExperienceModel(modelBuilder);
            this.AddEducationModeling(modelBuilder);
            this.AddDetailInfo(modelBuilder);
            this.AddPropertyBags(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void AddPRofileModel(ModelBuilder modelBuilder)
        {
            //for value objects we can also use owned entity types with json, we can store a value type in a json format in a single column
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");
                entity.HasKey(p => p.Id);
                //alternateKey has unique values, can be used as fk from other tables
                entity.HasAlternateKey(p => p.LastName);
                //entity.HasKey(p => new {p.Id, p.LastName });composite key
                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasColumnName("FirstName")
                    .HasMaxLength(50);

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasColumnName("LastName")
                    .HasMaxLength(50);

                entity.Property(p => p.Summary)
                    .IsRequired(false);

                entity.Property(p => p.IsPublic)
                    .HasDefaultValue(true);

                entity.Property(p => p.Visibility)
                    .IsRequired(false);

                entity.Property(p => p.DetailLevel)
                    .HasConversion(
                        v => v.ToString(),
                        v => (ProfileDetailLevel)Enum.Parse(typeof(ProfileDetailLevel), v)
                    )
                    .HasMaxLength(18)
                    .IsRequired();

                entity.Property(p => p.CreatedAt)
                    .IsRequired();

                entity.Property(p => p.UpdatedAt)
                    .IsRequired();

                //Shadow properties is something that EF use behind the scenes for example to
                //facilitate temporary tables, or to keep track of foreign keys that we have not mapped explicitly
                //we can use to access other fields that are not mapped to entities but exist in tables
                entity.HasOne(e => e.PersonalInfo)
                        .WithOne()
                        .HasForeignKey<DetailInfo>("ProfileId")
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Cascade);

                //.HasForeignKey(p => p.id)
                //.HasPrincipalKey(e => e.Id);

                //EF will create a shadow property, the fk in the language table
                //This shadow property is not defined in our Language entity domain class
                //Indexer properties will be used to create the join table in this many to many relationship
                entity.HasMany(p => p.LanguagesSpoken)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfileLanguages",
                        j => j.HasOne<Language>()
                            .WithMany()
                            .HasForeignKey("LanguageId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<Profile>()
                            .WithMany()
                            .HasForeignKey("ProfileId")
                            .OnDelete(DeleteBehavior.Cascade)
                        );
            });

            modelBuilder.Entity<QualifiedProfile>(entity =>
            {
                entity.ToTable("Profiles");
                entity.Property(p => p.DesiredJobRole)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.HasMany(p => p.Educations)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfileEducations",
                        j => j.HasOne<Education>()
                            .WithMany()
                            .HasForeignKey("EducationId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<QualifiedProfile>()
                            .WithMany()
                            .HasForeignKey("ProfileId")
                            .OnDelete(DeleteBehavior.Cascade)
                    );

                entity.HasMany(p => p.Experiences)
                    .WithOne()
                    .HasForeignKey("ProfileId")// We need to define fk to avoid adding aditional fk
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GeneralProfile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.Property(e => e.PersonalGoals)
                    .HasConversion(
                        goals => string.Join(",", goals),
                        goals => goals.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    )
                    .Metadata.SetValueComparer(stringArrayComparer);
                entity.HasMany(p => p.Experiences)
                    .WithOne() 
                    .HasForeignKey("ProfileId") // We need to define fk to avoid adding aditional fk
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentProfile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.Property(p => p.CareerGoals)
                    .IsRequired(false);

                entity.Property(p => p.Major)
                    .IsRequired(false);

                entity.HasMany(p => p.Educations)
                    .WithMany()
                    .UsingEntity<Dictionary<string, object>>(
                        "ProfileEducations",
                        j => j.HasOne<ResearchEducation>()
                            .WithMany()
                            .HasForeignKey("EducationId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<StudentProfile>()
                            .WithMany()
                            .HasForeignKey("ProfileId")
                            .OnDelete(DeleteBehavior.Cascade)
                    );
                entity.HasMany(p => p.Experiences)
                    .WithOne()
                    .HasForeignKey("ProfileId") // We need to define fk to avoid adding aditional fk
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //We can include nav properties by default
            //modelBuilder.Entity<Profile>()
            //    .Navigation(w => w.LanguagesSpoken)
            //    .AutoInclude();
        }

        private void AddLanguageModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Languages");
                entity.HasKey(p => p.Id);
                //In a system where you synchronize user profiles between databases, you can compare the checksum values to quickly identify records that need updates.
                //You can validate the integrity of user data during migrations or imports by ensuring that the checksum matches the recalculated value based on the migrated columns.
                //Auditing: In scenarios where you need to detect unauthorized or accidental changes to critical fields, the checksum can serve as an additional layer of protection.
                if (Database.IsSqlServer())
                {
                    entity.Property<byte[]>("Checksum")
                        .HasComputedColumnSql("CONVERT(VARBINARY(1024),CHECKSUM([Name],[Level]))");
                }
                else
                {
                    // SQLite doesn't support CHECKSUM, use a placeholder or omit the computed column for SQLite
                    entity.Property<byte[]>("Checksum")
                        .HasComputedColumnSql(null);
                }
                entity.Property(p => p.Name)
                    .HasConversion<string>();

                entity.Property(p => p.Level)
                    .HasConversion<string>();
            });
        }

        private void AddEducationModeling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("Educations");

                entity.HasKey(p => p.Id);
                
                entity.Property(p => p.StartDate)
                    .IsRequired();

                entity.Property(p => p.EndDate)
                    .IsRequired(false);

                entity.Property(p => p.Institution)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.HasDiscriminator<string>("Discriminator")
                    .HasValue<Education>("BaseEducation")
                    .HasValue<DegreeEducation>("DegreeEducation")
                    .HasValue<ResearchEducation>("ResearchEducation");

            });

            modelBuilder.Entity<DegreeEducation>(entity =>
            {
                entity.ToTable("Educations");

                entity.Property(p => p.Degree)
                    .HasConversion<string>();
            });

            modelBuilder.Entity<ResearchEducation>(entity =>
            {
                entity.ToTable("Educations");

                entity.Property(p => p.ProjectTitle)
                    .IsRequired();
                entity.Property(p => p.Supervisor)
                    .IsRequired(false);
                entity.Property(p => p.Summary)
                    .IsRequired();
            });
        }

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
        }

        private void AddJobExperienceModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobExperience>(entity =>
            {
                entity.ToTable("JobExperiences");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Company)
                    .IsRequired();
                entity.Property(p => p.StartDate)
                    .IsRequired();
                entity.Property(p => p.EndDate)
                    .IsRequired(false);
                entity.Property(p => p.Description)
                    .IsRequired(false);

                entity.HasDiscriminator<string>("Discriminator")
                    .HasValue("InternshipExperience")
                    .HasValue("WorkExperience");
            });

            modelBuilder.Entity<InternshipExperience>(entity =>
            {
                entity.ToTable("JobExperiences");
                entity.Property(p => p.Role)
                    .IsRequired();
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.ToTable("JobExperiences");
                entity.Property(p => p.Position)
                    .IsRequired();
            });
        }

        private void AddDetailInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetailInfo>(entity =>
            {
                entity.ToTable("DetailInfos");

                entity.HasKey(p => p.Id);
                entity.Property(p => p.Phone)
                    .HasMaxLength(20);
                entity.Property(p => p.Emails)
                    .IsRequired();
            });
        }

        //FLUENT API
        //Can be complex to setup
        //Keeps code clean
        //Support a large feature sets
        //Works when you can not nodify entity classes
        //DATA ANOTATIONS
        //Easy to setup
        //May clutter entity classes
        //smaller feature set
        //Only works when you have direct control on your classes

        //TPH table per herarchy
        //Can lead to empty columns
        //sql server sparce columns can be used to optimize space for empty columns
        //Sibiling entities can be mapped to shared columns
    }
}
