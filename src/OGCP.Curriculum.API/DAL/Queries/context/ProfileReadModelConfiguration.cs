using ArtForAll.Shared.Contracts.DDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Queries.context;

public class ProfileReadModelConfiguration : IEntityTypeConfiguration<ProfileReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileReadModel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.DesiredJobRole)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(e => e.PersonalGoals)
            .HasConversion(
                goals => string.Join(",", goals),
                goals => goals.Split(",", StringSplitOptions.RemoveEmptyEntries)
            );

        builder.Property(p => p.CareerGoals)
            .IsRequired(false);

        builder.Property(p => p.Major)
            .IsRequired(false);

        builder.HasMany(p => p.Educations)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfileEducations",
                j => j.HasOne<EducationReadModel>()
                    .WithMany()
                    .HasForeignKey("EducationId"),
                j => j.HasOne<ProfileReadModel>()
                    .WithMany()
                    .HasForeignKey("ProfileId"));

        builder.HasMany(p => p.Languages)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfileLanguages",
                j => j.HasOne<LanguageReadModel>()
                    .WithMany()
                    .HasForeignKey("LanguageId"),
                j => j.HasOne<ProfileReadModel>()
                    .WithMany()
                    .HasForeignKey("ProfileId"));

        builder.HasMany(p => p.WorkExp)
            .WithOne()
            .HasForeignKey("ProfileId");
    }
}
