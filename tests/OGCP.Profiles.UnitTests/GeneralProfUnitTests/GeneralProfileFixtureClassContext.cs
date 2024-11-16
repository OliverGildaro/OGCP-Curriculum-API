using Moq;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;

namespace OGCP.Profiles.UnitTests.GeneralProfUnitTests;

public class GeneralProfileFixtureClassContext : IClassFixture<GeneralProfileServiceFixture>
{
    private readonly GeneralProfileServiceFixture fixture;

    public GeneralProfileFixtureClassContext(GeneralProfileServiceFixture fixture)
    {
        this.fixture = fixture;
    }

    public static IEnumerable<object[]> Example_WithMethod()//since is static can be shared across diferent test clases
    {
        return new List<object[]>
        {
            new object[]{
                new CreateGeneralProfileRequest
                {
                    FirstName = "Oliver",
                    LastName = "Castro",
                    Summary = "Fullstack sumary",
                    PersonalGoals = new string[] { "Be the best", "Another" }
                },
                new CreateGeneralProfileRequest
                {
                    FirstName = "Cristian",
                    LastName = "Morato",
                    Summary = "Fullstack sumary",
                    PersonalGoals = new string[] { "Be the best", "Another" }
                },
            }
        };
    }

    [Theory]
    [MemberData(nameof(Example_WithMethod))]//this member data can be share across many unit tests
    public void Test1(CreateGeneralProfileRequest generalProfile)
    {
        var result = fixture.service.Create(generalProfile);
        Assert.True(result.IsSucces);
    }

    [Theory]
    [MemberData(nameof(Example_WithMethod))]//this member data can be share across many unit tests
    public void Test2()
    {
        var reques2 = new Mock<CreateGeneralProfileRequest>();
        fixture.service.Create(reques2.Object);
    }
}

public class GeneralProfileServiceFixture : IDisposable
{
    public Mock<IGeneralProfileRepository> repository { get; }
    public GeneralProfileService service { get; }

    public GeneralProfileServiceFixture()
    {
        repository = new Mock<IGeneralProfileRepository>();
        //repository.Setup(m => m.Add(It.IsAny<GeneralProfile>())).Returns(Result.Success);
        service = new GeneralProfileService(repository.Object);
    }

    public void Dispose()
    {
    }
}
