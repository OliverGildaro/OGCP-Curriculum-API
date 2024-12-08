using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculums.Reads.ProfileRepository.context.configurations;

public class LanguageReadModelConfiguration : IEntityTypeConfiguration<ProfileReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileReadModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
