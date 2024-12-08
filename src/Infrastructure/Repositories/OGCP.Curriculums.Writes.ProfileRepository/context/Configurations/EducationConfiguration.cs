using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> entity)
    {
        entity.ToTable("Educations");

        entity.HasKey(p => p.Id);

        entity.Property(p => p.StartDate)
            .HasColumnType("date")
            .IsRequired();

        entity.Property(p => p.EndDate)
            .HasColumnType("date")
            .IsRequired(false);

        entity.Property(p => p.Institution)
            .HasMaxLength(200)
            .IsRequired();

        entity.HasDiscriminator<string>("Discriminator")
            .HasValue<Education>("BaseEducation")
            .HasValue<DegreeEducation>("DegreeEducation")
            .HasValue<ResearchEducation>("ResearchEducation");
    }
}