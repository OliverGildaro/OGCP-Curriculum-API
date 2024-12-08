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
            .HasMaxLength(100)
            .IsRequired();
        entity.Property(p => p.Supervisor)
            .HasMaxLength(100)
            .IsRequired(false);
        entity.Property(p => p.Summary)
            .HasMaxLength(300)
            .IsRequired();

        //INDEXING
        //entity.HasIndex(p => new { p.ProjectTitle, p.Institution })
        //    .HasDatabaseName("IX_Educations_ProjectTitle_Institution_NC")
        //    .IncludeProperties(p => new { p.StartDate, p.EndDate, p.Supervisor });
    }
}