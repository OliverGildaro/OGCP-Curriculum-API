using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.services;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

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
        repository.Setup(x => x.SaveChangesAsync())
            .ReturnsAsync(() => 1);

        service = new ProfileService(repository.Object);
    }

    public void Dispose()
    {
    }

    //***** INLINE DATA *****//////
    [Theory]
    [InlineData(
        "Oliver",
        "Castro",
        "Fillstack dev",
        "Job here goal",
        "+59169554851",
        "gildaro.castro@gmail.com")]
    [InlineData("Cristian",
        "Morato",
        "Fillstack dev",
        "Job here goal",
        "+59169554851",
        "gildaro.castro@gmail.com")]
    public async Task Test1(string firstName,
        string lastName,
        string summanry, 
        string personalGoal,
        string phone,
        string email)
    {
        var phoneNumber = PhoneNumber.Parse(phone);
        var name = Name.CreateNew(firstName, lastName);
        var emailResult = Email.CreateNew(email);
        var request = GeneralProfile
            .Create(name.Value, summanry, new string[] { personalGoal }, phoneNumber, emailResult.Value);
        var resourceCreated = await service.CreateAsync(request.Value);

        Assert.IsType<Result>(resourceCreated);
        Assert.NotNull(resourceCreated);
    }
}
