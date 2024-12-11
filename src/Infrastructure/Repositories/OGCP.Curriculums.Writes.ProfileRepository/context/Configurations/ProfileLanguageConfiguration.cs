using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;

internal class ProfileLanguageConfiguration : IEntityTypeConfiguration<ProfileLanguage>
{
    public void Configure(EntityTypeBuilder<ProfileLanguage> entity)
    {
        entity.HasKey(p => new {p.ProfileId, p.LanguageId});

        entity.HasOne(p => p.Language)
            .WithMany()
            .HasForeignKey(p => p.LanguageId)
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.Property(typeof(List<LanguageSkill>), "_languageSkills")
                .HasColumnName("LanguageSkills")
                .HasColumnType("jsonb");
    }
}
