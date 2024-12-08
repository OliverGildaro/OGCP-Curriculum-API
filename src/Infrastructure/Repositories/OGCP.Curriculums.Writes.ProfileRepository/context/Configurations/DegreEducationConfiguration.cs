using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class DegreEducationConfiguration : IEntityTypeConfiguration<DegreeEducation>
{
    public void Configure(EntityTypeBuilder<DegreeEducation> entity)
    {

        entity.ToTable("Educations");

        entity.Property(p => p.Degree)
            .HasConversion<string>();
    }
}