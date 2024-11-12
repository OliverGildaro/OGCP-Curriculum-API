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

            base.OnModelCreating(modelBuilder);
        }
    }
}
