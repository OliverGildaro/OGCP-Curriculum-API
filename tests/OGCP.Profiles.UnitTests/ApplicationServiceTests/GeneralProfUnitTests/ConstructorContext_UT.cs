using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
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
    private IProfileService service;
    public ConstructorContext_UT()
    {
        //***** CONSTRUCTOR CONTEXT *****//////
        var repository = new Mock<IProfileWriteRepo>();
        repository.Setup(x => x.Add(It.IsAny<GeneralProfile>()))
            .Returns(Result.Success);
        repository.Setup(x => x.SaveChanges())
            .ReturnsAsync(() => 1);

        service = new ProfileService(repository.Object);
    }

    public void Dispose()
    {
    }

    //***** INLIENE DATA *****//////
    [Theory]
    [InlineData("Oliver", "Castro", "Fillstack dev", "Job here goal")]
    [InlineData("Cristian", "Morato", "Fillstack dev", "Job here goal")]
    public async Task Test1(string firstName, string lastName, string summanry, string personalGoal)
    {
        var request = GeneralProfile.Create(firstName, lastName, summanry, new string[] { personalGoal });
        var result = await service.Create(request.Value);

        //Assert.Equal(1, result);
        Assert.IsType<int>(result);
        Assert.NotNull(result);
    }
}
