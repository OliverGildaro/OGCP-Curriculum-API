using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.repositories
{
    public class DbProfileContext : DbContext
    {
        public DbProfileContext(DbContextOptions<DbProfileContext> dbContext)
            :base(dbContext)
        {
                
        }
        public DbSet<QualifiedProfile> QualifiedProfiles { get; set; }
        public DbSet<GeneralProfile> GeneralProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ValueComparer code helps Entity Framework Core track changes within string[]
            //Maybe value objects may help tp dp this calculation
            var stringArrayComparer = new ValueComparer<string[]>(
                (c1, c2) => c1.SequenceEqual(c2),                // Compare two arrays for equality
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),  // Generate a hash code for the array
                c => c.ToArray());                               // Create a snapshot of the array

            modelBuilder.Entity<GeneralProfile>()
                .Property(e => e.PersonalGoals)
                .HasConversion(
                    goals => string.Join(",", goals),
                    goals => goals.Split(",", StringSplitOptions.RemoveEmptyEntries)
                )
                .Metadata.SetValueComparer(stringArrayComparer);   // Set the custom value comparer

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
