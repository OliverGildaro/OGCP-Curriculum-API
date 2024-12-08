using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class JobExperienceConfiguration : IEntityTypeConfiguration<JobExperience>
{
    public void Configure(EntityTypeBuilder<JobExperience> entity)
    {
        entity.ToTable("JobExperiences");
        entity.HasKey(p => p.Id);
        entity.Property(p => p.Company)
            .IsRequired();
        entity.Property(p => p.StartDate)
            .IsRequired();
        entity.Property(p => p.EndDate)
            .IsRequired(false);
        entity.Property(p => p.Description)
            .IsRequired(false);

        entity.HasDiscriminator<string>("Discriminator")
            .HasValue("InternshipExperience")
            .HasValue("WorkExperience");
    }
}
