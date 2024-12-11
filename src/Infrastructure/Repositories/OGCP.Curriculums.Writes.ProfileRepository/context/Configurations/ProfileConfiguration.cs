using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculums.Writes.ProfileRepository.context.Configurations;
internal class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> entity)
    {
        //for value objects we can also use owned entity types with json, we can store a value type in a json format in a single column
        entity.ToTable("Profiles");

        //THis already create a clustered index, that is a b-tree
        //which enables searches, inserts, updates, and deletes in logarithmic amortized time
        //log₂(n)
        //Cada nivel del arbol reduce el tiempo de busqueda a la mitad
        entity.HasKey(p => p.Id);

        //alternateKey has unique values, can be used as fk from other tables
        //entity.HasAlternateKey(p => p.LastName);
        //entity.HasKey(p => new {p.Id, p.LastName });composite key

        entity.OwnsOne(p => p.Name, p =>
        {
            p.Property(pp => pp.GivenName)
                .IsRequired()
                .HasColumnName("GivenName")
                .HasMaxLength(100);

            p.Property(pp => pp.FamilyNames)
                .IsRequired()
                .HasColumnName("FamilyNames")
                .HasMaxLength(100);
        });

        entity.OwnsOne(p => p.Phone, p =>
        {
            p.Property(pp => pp.CountryCode)
                .IsRequired(false)
                .HasColumnName("CountryCode")
                .HasMaxLength(3);
            p.Property(pp => pp.Number)
                .IsRequired(false)
                .HasColumnName("Number")
                .HasMaxLength(20);
        });

        entity.Property(p => p.Email)
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(p => p.Value, p => Email.CreateNew(p).Value);

        entity.Property(p => p.Summary)
            .IsRequired(false);

        entity.Property(p => p.IsPublic)
            .HasDefaultValue(true);

        entity.Property(p => p.Visibility)
            .IsRequired(false);

        entity.Property(p => p.DetailLevel)
            .HasConversion(
                v => v.ToString(),
                v => (ProfileDetailLevel)Enum.Parse(typeof(ProfileDetailLevel), v)
            )
            .HasMaxLength(18)
            .IsRequired();

        entity.Property(p => p.CreatedAt)
            .IsRequired();

        entity.Property(p => p.UpdatedAt)
            .IsRequired();

        //Shadow properties is something that EF use behind the scenes for example to
        //facilitate temporary tables, or to keep track of foreign keys that we have not mapped explicitly
        //we can use to access other fields that are not mapped to entities but exist in tables
        entity.HasOne(e => e.PersonalInfo)
                .WithOne()
                .HasForeignKey<DetailInfo>("ProfileId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        //.HasForeignKey(p => p.id)
        //.HasPrincipalKey(e => e.Id);

        //EF will create a shadow property, the fk in the language table
        //This shadow property is not defined in our Language entity domain class
        //Indexer properties will be used to create the join table in this many to many relationship
        entity.HasMany(p => p.LanguagesSpoken)
            .WithOne()
            .HasForeignKey("ProfileId")
            .OnDelete(DeleteBehavior.Cascade);

        //INDEXING
        //INDEXING
        //FirstName values existiran en el indice
        //Tendremos un puntero a la tabla por si queremos traer mas datos
        entity.HasIndex(p => new { p.Name.GivenName, p.Name.FamilyNames })
            .HasDatabaseName("IX_Profiles_GivenName_FamilyNames")
            .IsUnique(false);
    }
}