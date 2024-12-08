using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
{
    public void Configure(EntityTypeBuilder<StudentProfile> entity)
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
    }
}