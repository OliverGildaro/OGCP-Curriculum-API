using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.DAL.Queries.Models;

namespace OGCP.Curriculum.API.DAL.Queries.context;

public class EducationReadModelConfiguration : IEntityTypeConfiguration<ProfileReadModel>
{
    public void Configure(EntityTypeBuilder<ProfileReadModel> builder)
    {
        builder.HasKey(x => x.Id);
    }

}
