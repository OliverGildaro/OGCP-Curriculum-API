﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OGCP.Curriculum.API.DAL.Mutations.context;

#nullable disable

namespace OGCP.Curriculums.Writes.ProfileRepository.Migrations
{
    [DbContext(typeof(ApplicationWriteDbContext))]
    [Migration("20241211182446_sp")]
    partial class sp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Certifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CertificationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateIssued")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssuingOrganization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Certifications");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.DetailInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Emails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("DetailInfos", (string)null);
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Educations", (string)null);

                    b.HasDiscriminator().HasValue("BaseEducation");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.JobExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("JobExperiences", (string)null);

                    b.HasDiscriminator().HasValue("WorkExperience");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Checksum")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("varbinary(max)")
                        .HasComputedColumnSql("CONVERT(VARBINARY(1024), CHECKSUM([Name], [Level]))");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages", (string)null);
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DetailLevel")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Visibility")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profiles", (string)null);

                    b.HasDiscriminator().HasValue("Profile");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OGCP.Curriculums.Core.DomainModel.profiles.ProfileLanguage", b =>
                {
                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.HasKey("ProfileId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ProfileLanguages", (string)null);
                });

            modelBuilder.Entity("ProfileEducations", b =>
                {
                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("EducationId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfileEducations");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.DegreeEducation", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.Education");

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasIndex("Degree", "Institution")
                        .HasDatabaseName("IX_Educations_Degree_Institution_NC");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("Degree", "Institution"), new[] { "StartDate", "EndDate", "Id" });

                    b.ToTable("Educations", (string)null);

                    b.HasDiscriminator().HasValue("DegreeEducation");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.ResearchEducation", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.Education");

                    b.Property<string>("ProjectTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Supervisor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasIndex("ProjectTitle", "Institution")
                        .HasDatabaseName("IX_Educations_ProjectTitle_Institution_NC");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("ProjectTitle", "Institution"), new[] { "StartDate", "EndDate", "Supervisor" });

                    b.ToTable("Educations", (string)null);

                    b.HasDiscriminator().HasValue("ResearchEducation");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.InternshipExperience", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.JobExperience");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("JobExperiences", (string)null);

                    b.HasDiscriminator().HasValue("InternshipExperience");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.WorkExperience", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.JobExperience");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("JobExperiences", (string)null);

                    b.HasDiscriminator().HasValue("WorkExperience");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.GeneralProfile", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.Profile");

                    b.Property<string>("PersonalGoals")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Profiles", (string)null);

                    b.HasDiscriminator().HasValue("GeneralProfile");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.QualifiedProfile", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.Profile");

                    b.Property<string>("DesiredJobRole")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ToTable("Profiles", (string)null);

                    b.HasDiscriminator().HasValue("QualifiedProfile");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.StudentProfile", b =>
                {
                    b.HasBaseType("OGCP.Curriculum.API.domainmodel.Profile");

                    b.Property<string>("CareerGoals")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Major")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Profiles", (string)null);

                    b.HasDiscriminator().HasValue("StudentProfile");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.DetailInfo", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.Profile", null)
                        .WithOne("PersonalInfo")
                        .HasForeignKey("OGCP.Curriculum.API.domainmodel.DetailInfo", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.JobExperience", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.QualifiedProfile", null)
                        .WithMany("Experiences")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.Profile", b =>
                {
                    b.OwnsOne("OGCP.Curriculums.Core.DomainModel.profiles.PhoneNumber", "Phone", b1 =>
                        {
                            b1.Property<int>("ProfileId")
                                .HasColumnType("int");

                            b1.Property<string>("CountryCode")
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)")
                                .HasColumnName("CountryCode");

                            b1.Property<string>("Number")
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)")
                                .HasColumnName("Number");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("OGCP.Curriculums.Core.DomainModel.valueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<int>("ProfileId")
                                .HasColumnType("int");

                            b1.Property<string>("FamilyNames")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("FamilyNames");

                            b1.Property<string>("GivenName")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("GivenName");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Phone")
                        .IsRequired();
                });

            modelBuilder.Entity("OGCP.Curriculums.Core.DomainModel.profiles.ProfileLanguage", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OGCP.Curriculum.API.domainmodel.Profile", "Profile")
                        .WithMany("LanguagesSpoken")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("OGCP.Curriculums.Core.DomainModel.valueObjects.LanguageSkill", "LanguageSkills", b1 =>
                        {
                            b1.Property<int>("ProfileLanguageProfileId")
                                .HasColumnType("int");

                            b1.Property<int>("ProfileLanguageLanguageId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("Level")
                                .HasColumnType("int");

                            b1.Property<int>("Skill")
                                .HasColumnType("int");

                            b1.HasKey("ProfileLanguageProfileId", "ProfileLanguageLanguageId", "Id");

                            b1.ToTable("ProfileLanguages");

                            b1.ToJson("LangSkills");

                            b1.WithOwner()
                                .HasForeignKey("ProfileLanguageProfileId", "ProfileLanguageLanguageId");
                        });

                    b.Navigation("Language");

                    b.Navigation("LanguageSkills");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("ProfileEducations", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.Education", null)
                        .WithMany()
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OGCP.Curriculum.API.domainmodel.ResearchEducation", null)
                        .WithMany()
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OGCP.Curriculum.API.domainmodel.QualifiedProfile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OGCP.Curriculum.API.domainmodel.StudentProfile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.InternshipExperience", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.StudentProfile", null)
                        .WithMany("Experiences")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.WorkExperience", b =>
                {
                    b.HasOne("OGCP.Curriculum.API.domainmodel.GeneralProfile", null)
                        .WithMany("Experiences")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.Profile", b =>
                {
                    b.Navigation("LanguagesSpoken");

                    b.Navigation("PersonalInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.GeneralProfile", b =>
                {
                    b.Navigation("Experiences");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.QualifiedProfile", b =>
                {
                    b.Navigation("Experiences");
                });

            modelBuilder.Entity("OGCP.Curriculum.API.domainmodel.StudentProfile", b =>
                {
                    b.Navigation("Experiences");
                });
#pragma warning restore 612, 618
        }
    }
}