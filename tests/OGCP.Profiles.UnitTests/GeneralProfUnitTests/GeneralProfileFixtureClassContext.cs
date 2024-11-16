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

    [Theory]
    [ClassData(typeof(CreateGeneralProfileRequestTestData))]
    public void Test1(CreateGeneralProfileRequest generalProfile)
    {
        var result = fixture.service.Create(generalProfile);
        Assert.True(result.IsSucces);
    }

    [Fact]
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


public class CreateGeneralProfileRequestTestData : TheoryData<CreateGeneralProfileRequest>
{
    public CreateGeneralProfileRequestTestData()
    {
        this.Add(
            new CreateGeneralProfileRequest
            {
                FirstName = "Oliver",
                LastName = "Castro",
                Summary = "Fullstack sumary",
                PersonalGoals = new string[] { "Be the best", "Another" }
            });
    }
}