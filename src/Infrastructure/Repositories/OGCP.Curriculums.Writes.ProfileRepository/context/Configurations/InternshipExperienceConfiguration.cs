using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;

internal class InternshipExperienceConfiguration : IEntityTypeConfiguration<InternshipExperience>
{
    public void Configure(EntityTypeBuilder<InternshipExperience> entity)
    {
        entity.ToTable("JobExperiences");
        entity.Property(p => p.Role)
            .IsRequired();
    }
}
