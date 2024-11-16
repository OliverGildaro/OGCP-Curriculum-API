using Moq;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Profiles.UnitTests.GeneralProfUnitTests;

//Just decorating with the collection fixture will ensure that the test context will be provided here
[Collection("GeneralProfileServiceCollection")]
public class GeneralProfileFixtureCollectionContext
{
    private readonly GeneralProfileServiceFixture fixture;

    public GeneralProfileFixtureCollectionContext(GeneralProfileServiceFixture fixture)
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

    [Theory]
    [ClassData(typeof(CreateGeneralProfileRequestTestData))]
    public void Test2()
    {
        var reques2 = new Mock<CreateGeneralProfileRequest>();
        fixture.service.Create(reques2.Object);
    }
}

//We are wrapping the fixture class aproach
//now we can reuse the same instance to test classes
[CollectionDefinition("GeneralProfileServiceCollection")]
public class GeneralProfileServiceCollectionFixture
    : ICollectionFixture<GeneralProfileServiceFixture>
{

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
