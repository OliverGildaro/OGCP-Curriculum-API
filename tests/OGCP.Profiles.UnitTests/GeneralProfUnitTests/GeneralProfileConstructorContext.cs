using Moq;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Profiles.UnitTests.GeneralProfUnitTests;

public class GeneralProfileConstructorContext : IDisposable
{
    private IGeneralProfileService service;
    public GeneralProfileConstructorContext()
    {
        var repository = new Mock<IGeneralProfileRepository>();
        service = new GeneralProfileService(repository.Object);
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    [Theory]
    [InlineData("Oliver", "Castro", "Fillstack dev", "Job here goal")]
    [InlineData("Cristian", "Morato", "Fillstack dev", "Job here goal")]
    public void Test1(string firstName, string lastName, string summanry, string personalGoal)
    {
        var request = new CreateGeneralProfileRequest {
            FirstName = firstName,
            LastName = lastName,
            Summary = summanry,
            PersonalGoals = new string[] {personalGoal}
        };
        service.Create(request);
    }

    [Theory]
    [InlineData("Oliver", "Castro", "Fillstack dev", "Job here goal")]
    [InlineData("Cristian", "Morato", "Fillstack dev", "Job here goal")]
    public void Test2(string firstName, string lastName, string summanry, string personalGoal)
    {
        var request = new CreateGeneralProfileRequest
        {
            FirstName = firstName,
            LastName = lastName,
            Summary = summanry,
            PersonalGoals = new string[] { personalGoal }
        };

        service.Create(request);
    }
}
