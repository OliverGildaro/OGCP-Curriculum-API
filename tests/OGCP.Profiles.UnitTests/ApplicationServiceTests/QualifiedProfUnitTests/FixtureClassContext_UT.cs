using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;
using System.Runtime.Serialization;

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
            QualifiedProfile.Create("Oliver", "Castro", "Fullstack", "Backend").Value,
            QualifiedProfile.Create("Alvaro", "Castro", "Fullstack", "Backend and architect").Value,
        };
    }

    [Theory]
    [MemberData(nameof(GetQualifiedProfileData))]
    public async Task Test2(QualifiedProfile request)
    {
        var result = await context.service.Create(request);

        Assert.IsType<int>(result);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task test22()
    {
        var result = await context.service.Get(1);

        Assert.IsType<QualifiedProfile>(result);
        Assert.Equal("Oliver", result.FirstName);
        Assert.Equal("Castro", result.LastName);
        Assert.NotNull(result.Summary);
        Assert.NotNull(result);
        Assert.StartsWith("O", result.FirstName);
        Assert.Contains("str", result.LastName);
    }
}


public class QualifiedProfileServiceFIxtureClass
{
    public QualifiedProfileService service { get; }

    public QualifiedProfileServiceFIxtureClass()
    {
        var repo = new Mock<IQualifiedProfileWriteRepo>();
        repo.Setup(m => m.Add(It.IsAny<QualifiedProfile>()))
            .Returns(Result.Success);

        repo.Setup(x => x.SaveChanges())
            .ReturnsAsync(() => 1);

        repo.Setup(m => m.Find(It.IsAny<int>()))
                .ReturnsAsync(QualifiedProfile
                    .Create("Oliver", "Castro", "I am bla bla", "Fullstack software dev").Value);

        service = new QualifiedProfileService(repo.Object);
    }
}
