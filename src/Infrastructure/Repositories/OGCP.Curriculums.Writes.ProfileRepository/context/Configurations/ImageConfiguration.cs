using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.Images;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations
{
    internal class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> entity)
        {
            entity.ToTable("Images")
                .HasKey(p => p.Id);

            entity.Property(p => p.ProfileId) // Foreign key to Event
                 .IsRequired();

            entity.Property(p => p.ContentType)
                .IsUnicode()
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(p => p.FileName)
                .IsUnicode()
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}
