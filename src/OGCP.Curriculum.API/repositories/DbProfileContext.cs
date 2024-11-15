using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OGCP.Curriculum.API.domainModel;
using OGCP.Curriculum.API.models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    .HasDefaultValue(ProfileDetailLevel.Minimal)
                    .IsRequired();

                entity.Property(p => p.CreatedAt)
                    .IsRequired();

                entity.Property(p => p.UpdatedAt)
                .IsRequired();

                entity.HasMany(p => p.LanguagesSpoken)
                    .WithMany();

                entity.HasOne(p => p.PersonalInfo)
                    .WithOne();
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


            modelBuilder.Entity<QualifiedProfile>(entity =>
            {
                entity.Property(p => p.DesiredJobRole)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.HasMany(p => p.Education)
                    .WithOne();

                entity.HasMany(p => p.WorkExperience)
                    .WithOne();        
            });

            modelBuilder.Entity<StudentProfile>(entity =>
            {
                entity.Property(p => p.CareerGoals)
                    .IsRequired(false);

                entity.Property(p => p.Major)
                    .IsRequired(false);

                entity.HasMany(p => p.Internships)
                    .WithOne();

                entity.HasMany(p => p.ResearchEducation)
                    .WithOne();
            });














            modelBuilder.Entity<Education>()
                .HasDiscriminator<string>("EducationType")
                .HasValue<Education>("BaseEducation")
                .HasValue<DegreeEducation>("DegreeEducation")
                .HasValue<ResearchEducation>("ResearchEducation");


            base.OnModelCreating(modelBuilder);
        }
        //FLUENT API
        //Can be complex to setup
        //Keeps code clean
        //Support a large feature sets
        //Works when you cant nodufy entity classes
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
