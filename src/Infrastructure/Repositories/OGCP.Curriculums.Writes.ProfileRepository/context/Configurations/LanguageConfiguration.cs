using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> entity)
    {
        entity.ToTable("Languages");
        
        entity.HasKey(p => p.Id);

        entity.Property(p => p.Name)
            .HasConversion<string>();

        entity.Property(p => p.Level)
            .HasConversion<string>();
    }
}