using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;

namespace OGCP.Profiles.UnitTests.serviceTests.GeneralProfUnitTests;

//***** MEMBER DATA *****//////
//***** FIXTURE CLASS CONTEXT *****//////
public class FixtureClassContext_UT : IClassFixture<GeneralProfileServiceFixtureClass>
{
    private readonly GeneralProfileServiceFixtureClass fixture;

    public FixtureClassContext_UT(GeneralProfileServiceFixtureClass fixture)
    {
        this.fixture = fixture;
    }

    //***** MEMBER DATA *****//////
    public static TheoryData<CreateGeneralProfileRequest> Example_WithMethod()//since is static can be shared across diferent test clases
    {
        return new TheoryData<CreateGeneralProfileRequest>
        {
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
        };
    }

    [Theory]
    [MemberData(nameof(Example_WithMethod))]//this member data can be share across many unit tests
    public void Test1(CreateGeneralProfileRequest generalProfile)
    {
        var result = fixture.service.Create(generalProfile);

        Assert.True(result.IsSucces);
        Assert.IsType<Result>(result);
        Assert.Empty(result.Message);
        Assert.NotNull(result);
    }

    //here I have not been able to use fixture class context
    [Fact]
    public void Test2()
    {
        var mockRepo = new Mock<IGeneralProfileRepository>();

        mockRepo
              .Setup(m => m.Find())
              .Returns(new List<GeneralProfile>()
              {
                        GeneralProfile.Create("Oliver", "Castro", "Fullstack dev", new string[]{ "goal"}).Value,
                        GeneralProfile.Create("Cristian", "Morato", "Fullstack dev senior", new string[]{ "goal"}).Value
              });

        var service = new GeneralProfileService(mockRepo.Object);

        var profiles = service.Get().ToArray();

        Assert.Equal(2, profiles.Count());

        Assert.Collection(profiles,
            profile =>
            {
                Assert.Equal("Oliver", profile.FirstName);
                Assert.Equal("Castro", profile.LastName);
                Assert.Equal("Fullstack dev", profile.Summary);
                Assert.Contains("goal", profile.PersonalGoals);
                Assert.StartsWith("Full", profile.Summary);
            },
            profile =>
            {
                Assert.Equal("Cristian", profile.FirstName);
                Assert.Equal("Morato", profile.LastName);
                Assert.Equal("Fullstack dev senior", profile.Summary);
                Assert.Contains("goal", profile.PersonalGoals);
                Assert.StartsWith("Full", profile.Summary);
            });

        Assert.All(profiles, profile =>
        {
            Assert.NotNull(profile.FirstName);
            Assert.NotNull(profile.LastName);
            Assert.NotNull(profile.PersonalGoals);
        });

        Assert.Contains(profiles, p => p.FirstName == "Oliver");
        Assert.DoesNotContain(profiles, p => p.FirstName == "Nonexistent");

        mockRepo.Verify(m => m.Find(), Times.Once);
    }
}

//***** FIXTURE CLASS CONTEXT *****//////
public class GeneralProfileServiceFixtureClass : IDisposable
{
    public Mock<IGeneralProfileRepository> repository { get; }
    public GeneralProfileService service { get; }

    public GeneralProfileServiceFixtureClass()
    {
        repository = new Mock<IGeneralProfileRepository>();
        repository
            .Setup(m => m.Add(It.IsAny<GeneralProfile>()))
            .Returns(Result.Success);

        repository
            .Setup(m => m.SaveChanges())
            .Returns(Result.Success);

        repository
            .Setup(m => m.Find())
            .Returns(new List<GeneralProfile>()
            {
                GeneralProfile.Create("Oliver", "Castro", "Fulsstack dev", new string[]{ "goal"}).Value,
                GeneralProfile.Create("Cristian", "Morato", "Fulsstack dev senior", new string[]{ "goal"}).Value
            });

        service = new GeneralProfileService(repository.Object);
    }

    public void Dispose()
    {
    }
}
