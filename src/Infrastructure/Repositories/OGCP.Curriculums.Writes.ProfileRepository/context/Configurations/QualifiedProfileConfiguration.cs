using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class QualifiedProfileConfiguration : IEntityTypeConfiguration<QualifiedProfile>
{
    public void Configure(EntityTypeBuilder<QualifiedProfile> entity)
    {
        entity.ToTable("Profiles");
        entity.Property(p => p.DesiredJobRole)
            .HasMaxLength(200)
            .IsRequired(false);

        entity.HasMany(p => p.Educations)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProfileEducations",
                j => j.HasOne<Education>()
                    .WithMany()
                    .HasForeignKey("EducationId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<QualifiedProfile>()
                    .WithMany()
                    .HasForeignKey("ProfileId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        entity.HasMany(p => p.Experiences)
            .WithOne()
            .HasForeignKey("ProfileId")// We need to define fk to avoid adding aditional fk
            .OnDelete(DeleteBehavior.Cascade);
        //We can include nav properties by default
        //By this can result in a product cartesian explosion
        //When you AutoInclude more than one relation
        //modelBuilder.Entity<Profile>()
        //    .Navigation(w => w.LanguagesSpoken)
        //    .AutoInclude();
    }
}