using ArtForAll.Shared.ErrorHandler;
using Moq;
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

    public static TheoryData<CreateQualifiedProfileRequest> GetQualifiedProfileData()
    {
        return new TheoryData<CreateQualifiedProfileRequest>
        {
            new CreateQualifiedProfileRequest
            {
                FirstName = "Oliver",
                LastName = "Castro",
                Summary = "Fullstack",
                DesiredJobRole ="Backend"
            },
            new CreateQualifiedProfileRequest
            {
                FirstName = "Alvaro",
                LastName = "Castro",
                Summary = "Fullstack",
                DesiredJobRole ="Backend and architect"
            }
        };
    }

    [Theory]
    [MemberData(nameof(GetQualifiedProfileData))]
    public void Test2(CreateQualifiedProfileRequest request)
    {
        var result = context.service.Create(request);

        Assert.IsType<Result>(result);
        Assert.NotNull(result);
        Assert.True(result.IsSucces);
        Assert.Empty(result.Message);
    }

    [Fact]
    public void test22()
    {
        var result = context.service.Get(1);

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
        var repo = new Mock<IQualifiedProfileRepository>();
        repo.Setup(m => m.Add(It.IsAny<QualifiedProfile>()))
            .Returns(Result.Success);

        repo.Setup(m => m.Find(It.IsAny<int>()))
                .Returns(QualifiedProfile
                    .Create("Oliver", "Castro", "I am bla bla", "Fullstack software dev").Value);

        service = new QualifiedProfileService(repo.Object);
    }
}
