using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.utils;
using System.Reflection.Metadata;
namespace OGCP.Curriculum.API.repositories
{
    public class DbProfileContext : DbContext
    {

        //ValueComparer code helps Entity Framework Core track changes within string[]
        //Maybe value objects may help tp dp this calculation
        private ValueComparer<string[]> stringArrayComparer = new ValueComparer<string[]>(
            (c1, c2) => c1.SequenceEqual(c2),                // Compare two arrays for equality
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),  // Generate a hash code for the array
            c => c.ToArray());                               // Create a snapshot of the array

        public DbProfileContext(DbContextOptions<DbProfileContext> dbContext)
            : base(dbContext)
        {

        }

        public DbSet<QualifiedProfile> QualifiedProfiles { get; set; }
        public DbSet<GeneralProfile> GeneralProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        //public virtual DbSet<Dictionary<string, object>> Certifications =>
        //        Set<Dictionary<string, object>>("Certification");

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<EducationLevel>()
                .HaveConversion<EducationLevelConverter>();

            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //for value objects we can also use owned entity types with json, we can store a value type in a json format in a single column
            modelBuilder.Entity<Profile>(entity =>
            {
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
                    .HasConversion<string>()
                    //.HasDefaultValue(ProfileDetailLevel.Minimal.ToString())
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
                        .IsRequired();
                //.HasForeignKey(p => p.id)
                //.HasPrincipalKey(e => e.Id);

                //EF will create a shadow property, the fk in the language table
                //This shadow property is not defined in our Language entity domain class
                //Indexer properties will be used to create the join table in this many to many relationship
                entity.HasMany(p => p.LanguagesSpoken)
                    .WithMany();

                entity.HasMany(p => p.WorkExperience)
                    .WithOne();
            });

            modelBuilder.Entity<QualifiedProfile>(entity =>
            {
                entity.Property(p => p.DesiredJobRole)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.HasMany(p => p.Educations) // Access the collection from `EducationList`
                      .WithMany() // Assuming `QualifiedProfiles` exists in `Education`
                      .UsingEntity<Dictionary<string, object>>(
                          "QualifiedProfileEducation", // Name of the join table
                          j => j.HasOne<Education>()
                                .WithMany()
                                .HasForeignKey("EducationId"),
                          j => j.HasOne<QualifiedProfile>()
                                .WithMany()
                                .HasForeignKey("QualifiedProfileId"));


            });

            modelBuilder.Entity<GeneralProfile>(entity =>
            {
                entity.Property(e => e.PersonalGoals)
                    .HasConversion(
                        goals => string.Join(",", goals),
                        goals => goals.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    )
                    .Metadata.SetValueComparer(stringArrayComparer);
            });

            modelBuilder.Entity<StudentProfile>(entity =>
            {
                entity.Property(p => p.CareerGoals)
                    .IsRequired(false);

                entity.Property(p => p.Major)
                    .IsRequired(false);
                entity.HasMany(p => p.ResearchEducation)
                    .WithOne();
            });

            modelBuilder.Entity<Language>(entity =>
            {
                //In a system where you synchronize user profiles between databases, you can compare the checksum values to quickly identify records that need updates.
                //You can validate the integrity of user data during migrations or imports by ensuring that the checksum matches the recalculated value based on the migrated columns.
                //Auditing: In scenarios where you need to detect unauthorized or accidental changes to critical fields, the checksum can serve as an additional layer of protection.
                entity.Property<byte[]>("Checksum")
                    .HasComputedColumnSql("CONVERT(VARBINARY(1024),CHECKSUM([Name],[Level]))");
                entity.Property(p => p.Name)
                    .HasConversion<string>();
                    //.HasDefaultValue(Languages.Spanish.ToString());

                entity.Property(p => p.Level)
                    .HasConversion<string>();
                    //.HasDefaultValue(ProficiencyLevel.Beginner.ToString());
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<JobExperience>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Company)
                    .IsRequired();
                entity.Property(p => p.StartDate)
                    .IsRequired();
                entity.Property(p => p.EndDate)
                    .IsRequired(false);
                entity.Property(p => p.Description)
                    .IsRequired(false);
            });

            modelBuilder.Entity<InternshipExperience>(entity =>
            {
                entity.Property(p => p.Role)
                    .IsRequired();
            });


            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.Property(p => p.Position)
                    .IsRequired();
            });

            //modelBuilder.Entity<Education>()
            //    .HasDiscriminator<string>("EducationType")
            //    .HasValue<Education>("BaseEducation")
            //    .HasValue<DegreeEducation>("DegreeEducation")
            //    .HasValue<ResearchEducation>("ResearchEducation");

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.StartDate)
                    .IsRequired();
                entity.Property(p => p.EndDate)
                    .IsRequired(false);
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Institution)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.HasDiscriminator<string>("EducationType")
                    .HasValue<Education>("BaseEducation")
                    .HasValue<DegreeEducation>("DegreeEducation")
                    .HasValue<ResearchEducation>("ResearchEducation");
            });

            modelBuilder.Entity<DegreeEducation>(entity =>
            {
                entity.Property(p => p.Degree)
                    .HasConversion<string>();
            });

            modelBuilder.Entity<ResearchEducation>(entity =>
            {
                entity.Property(p => p.ProjectTitle)
                    .IsRequired();
                entity.Property(p => p.Supervisor)
                    .IsRequired(false);
                entity.Property(p => p.Summary)
                    .IsRequired();
            });

            modelBuilder.Entity<DetailInfo>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Phone)
                    .HasMaxLength(20);
                entity.Property(p => p.Emails)
                    .IsRequired();
            });

            //We do not need to map this to an CLR class
            //Easy to add metadata without modifying database schema
            //We can add attributes in a flexible way
            //This is called indexer properties and allow us to create property bags.
            //In a many to many relationship we create a property bag, witout creating the intermediate join class.
            //We could create a json object in settings.json to setup this property bag based on the json key/values
            //modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
            //    "Certification",
            //    entity =>
            //    {
            //        entity.Property<int>("Id");
            //        entity.Property<int>("ProfileId");
            //        entity.Property<string>("CertificationName");
            //        entity.Property<string>("IssuingOrganization");
            //        entity.Property<DateTime>("DateIssued");
            //        entity.Property<DateTime?>("ExpirationDate");
            //        entity.Property<string>("Description");
            //    });


            base.OnModelCreating(modelBuilder);
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
