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

    [Fact]
    public void Test1()
    {
        var request = new Mock<CreateGeneralProfileRequest>();
        service.Create(request.Object);
    }

    [Fact]
    public void Test2()
    {
        var reques2 = new Mock<CreateGeneralProfileRequest>();
        service.Create(reques2.Object);
    }
}
