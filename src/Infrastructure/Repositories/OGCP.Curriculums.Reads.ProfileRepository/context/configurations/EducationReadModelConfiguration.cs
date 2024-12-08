using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculums.Reads.ProfileRepository.context.configurations;

public class EducationReadModelConfiguration : IEntityTypeConfiguration<EducationReadModel>
{
    public void Configure(EntityTypeBuilder<EducationReadModel> builder)
    {
        builder.HasKey(x => x.Id);


    }

}
