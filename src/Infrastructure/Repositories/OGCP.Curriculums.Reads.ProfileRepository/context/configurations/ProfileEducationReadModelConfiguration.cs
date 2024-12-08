using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculums.Reads.ProfileRepository.context.configurations;

public class ProfileEducationReadModelConfiguration : IEntityTypeConfiguration<ProfileEducationReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileEducationReadModel> builder)
    {
        builder.HasKey(p => new { p.ProfileId, p.EducationId });
    }
}
