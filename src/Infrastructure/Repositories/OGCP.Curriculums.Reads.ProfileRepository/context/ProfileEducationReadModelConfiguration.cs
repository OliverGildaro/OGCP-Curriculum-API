using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculums.Reads.ProfileRepository.Models;

namespace OGCP.Curriculum.API.DAL.Queries.context;

public class ProfileEducationReadModelConfiguration : IEntityTypeConfiguration<ProfileEducationReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileEducationReadModel> builder)
    {
        builder.HasKey(p => new { p.ProfileId, p.EducationId });
    }

}
