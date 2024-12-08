using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class ResearchEducationConfiguration : IEntityTypeConfiguration<ResearchEducation>
{
    public void Configure(EntityTypeBuilder<ResearchEducation> entity)
    {
        entity.ToTable("Educations");

        entity.Property(p => p.ProjectTitle)
            .IsRequired();
        entity.Property(p => p.Supervisor)
            .IsRequired(false);
        entity.Property(p => p.Summary)
            .IsRequired();
    }
}