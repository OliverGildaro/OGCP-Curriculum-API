using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;

internal class DetailInfoConfiguration : IEntityTypeConfiguration<DetailInfo>
{
    public void Configure(EntityTypeBuilder<DetailInfo> entity)
    {
        entity.ToTable("DetailInfos");

        entity.HasKey(p => p.Id);
        entity.Property(p => p.Phone)
            .HasMaxLength(20);
        entity.Property(p => p.Emails)
            .IsRequired();
    }
}
