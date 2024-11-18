using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Profiles.UnitTests.serviceTests.GeneralProfUnitTests;

//***** INLIENE DATA *****//////
//***** CONSTRUCTOR CONTEXT *****//////
public class ConstructorContext_UT : IDisposable
{
    private IGeneralProfileService service;
    public ConstructorContext_UT()
    {
        //***** CONSTRUCTOR CONTEXT *****//////
        var repository = new Mock<IGeneralProfileRepository>();
        repository.Setup(x => x.Add(It.IsAny<GeneralProfile>()))
            .Returns(Result.Success);
        repository.Setup(x => x.SaveChanges())
            .Returns(Result.Success);

        service = new GeneralProfileService(repository.Object);
    }

    public void Dispose()
    {
    }

    //***** INLIENE DATA *****//////
    [Theory]
    [InlineData("Oliver", "Castro", "Fillstack dev", "Job here goal")]
    [InlineData("Cristian", "Morato", "Fillstack dev", "Job here goal")]
    public void Test1(string firstName, string lastName, string summanry, string personalGoal)
    {
        var request = new CreateGeneralProfileRequest
        {
            FirstName = firstName,
            LastName = lastName,
            Summary = summanry,
            PersonalGoals = new string[] { personalGoal }
        };
        var result = service.Create(request);

        Assert.True(result.IsSucces);
        Assert.IsType<Result>(result);
        Assert.Empty(result.Message);
        Assert.NotNull(result);
    }
}
