using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class DegreEducationConfiguration : IEntityTypeConfiguration<DegreeEducation>
{
    public void Configure(EntityTypeBuilder<DegreeEducation> entity)
    {

        entity.ToTable("Educations");

        entity.Property(p => p.Degree)
            .HasMaxLength(100)
            .HasConversion<string>();

        //INDEXING
        //Degree y Institution values existiran en el indice junto a StartDate, EndDate...
        //Igual tendremos un puntero para traer los otros fields de la tabla
        //entity.HasIndex(p => new { p.Degree, p.Institution })
        //    .HasDatabaseName("IX_Educations_Degree_Institution_NC")
        //    .IncludeProperties(p => new { p.StartDate, p.EndDate, p.Id });
    }
}