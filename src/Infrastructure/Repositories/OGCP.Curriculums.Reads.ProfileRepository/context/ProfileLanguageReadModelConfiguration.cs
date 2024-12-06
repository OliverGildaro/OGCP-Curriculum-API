using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.DAL.Queries.context;

public class ProfileLanguageReadModelConfiguration : IEntityTypeConfiguration<ProfileLanguageReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileLanguageReadModel> builder)
    {
        builder.HasKey(p => new { p.ProfileId, p.LanguageId });
    }

}
