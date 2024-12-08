using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class GeneralProfileConfiguration : IEntityTypeConfiguration<GeneralProfile>
{
    //ValueComparer code helps Entity Framework Core track changes within string[]
    //Maybe value objects may help tp dp this calculation
    private ValueComparer<string[]> stringArrayComparer = new ValueComparer<string[]>(
        (c1, c2) => c1.SequenceEqual(c2),                // Compare two arrays for equality
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),  // Generate a hash code for the array
        c => c.ToArray());

    public void Configure(EntityTypeBuilder<GeneralProfile> entity)
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
    }
}