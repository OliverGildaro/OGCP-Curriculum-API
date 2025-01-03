﻿using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Profiles.UnitTests.serviceTests.QualifiedProfUnitTests;

public class FixtureClassContext_UT : IClassFixture<QualifiedProfileServiceFIxtureClass>
{
    private readonly QualifiedProfileServiceFIxtureClass context;

    public FixtureClassContext_UT(QualifiedProfileServiceFIxtureClass context)
    {
        this.context = context;
    }

    public static TheoryData<QualifiedProfile> GetQualifiedProfileData()
    {
        return new TheoryData<QualifiedProfile>
        {
            QualifiedProfile.Create(                
                Name.CreateNew("Oliver", "Castro").Value,
                "Fullstack",
                "Backend",
                PhoneNumber.CreateNew("591", "69554851").Value,
                Email.CreateNew("gildaro.castro@gmai.com").Value).Value,
            QualifiedProfile.Create(
                Name.CreateNew("Alvaro", "Castro").Value,
                "Fullstack", "Backend and architect",
                PhoneNumber.CreateNew("591", "69554851").Value,
                Email.CreateNew("gildaro.castro@gmai.com").Value).Value,
        };
    }

    [Theory]
    [MemberData(nameof(GetQualifiedProfileData))]
    public async Task Test2(QualifiedProfile request)
    {
        var result = await context.service.CreateAsync(request);

        Assert.IsType<Result>(result);
    }

    //[Fact]
    //public async Task test22()
    //{
    //    var result = await context.service.GetAsync(1);

    //    Assert.IsType<QualifiedProfile>(result);
    //    Assert.Equal("Oliver", result.FirstName);
    //    Assert.Equal("Castro", result.LastName);
    //    Assert.NotNull(result.Summary);
    //    Assert.NotNull(result);
    //    Assert.StartsWith("O", result.FirstName);
    //    Assert.Contains("str", result.LastName);
    //}
}


public class QualifiedProfileServiceFIxtureClass
{
    public ProfileService service { get; }

    public QualifiedProfileServiceFIxtureClass()
    {
        var repo = new Mock<IProfileWriteRepo>();
        repo.Setup(m => m.Add(It.IsAny<QualifiedProfile>()))
            .Returns(Result.Success);

        repo.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(() => 1);

        repo.Setup(m => m.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(QualifiedProfile
                    .Create(
                        Name.CreateNew("Oliver", "Castro").Value,
                        "I am bla bla",
                        "Fullstack software dev",
                        PhoneNumber.CreateNew("591", "69554851").Value,
                        Email.CreateNew("gildaro.castro@gmai.com").Value).Value);

        service = new ProfileService(repo.Object);
    }
}
