using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;

internal class ProfileLanguageConfiguration : IEntityTypeConfiguration<ProfileLanguage>
{
    public void Configure(EntityTypeBuilder<ProfileLanguage> entity)
    {
        entity.ToTable("ProfileLanguages");
        entity.HasKey(p => new { p.ProfileId, p.LanguageId });

        entity.Property(p => p.ProfileId).IsRequired();
        entity.Property(p => p.LanguageId).IsRequired();

        entity.HasOne(p => p.Language)
            .WithMany()
            .HasForeignKey(p => p.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);

        //entity.HasOne(p => p.Profile)
        //    .WithMany()
        //    .HasForeignKey(p => p.ProfileId)
        //    .OnDelete(DeleteBehavior.Cascade);

        entity.OwnsMany(p => p.LanguageSkills, builder =>
        {
            builder.ToJson("LangSkills"); // Map the collection to a JSON column
        });
    }
}
