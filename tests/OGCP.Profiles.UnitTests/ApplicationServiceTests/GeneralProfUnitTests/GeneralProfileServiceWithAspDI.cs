//using Microsoft.Extensions.DependencyInjection;
//using Moq;
//using OGCP.Curriculum.API.dtos;
//using OGCP.Curriculum.API.repositories;
//using OGCP.Curriculum.API.repositories.interfaces;
//using OGCP.Curriculum.API.services;
//using OGCP.Curriculum.API.services.interfaces;

//namespace OGCP.Profiles.UnitTests.GeneralProfUnitTests;


//public class GeneralProfileServiceWithAspDI : IClassFixture<GeneralProfileServiceFixtureWithAspDI>
//{
//    private readonly GeneralProfileServiceFixtureWithAspDI fixtureWithAspDI;

//    public GeneralProfileServiceWithAspDI(GeneralProfileServiceFixtureWithAspDI fixtureWithAspDI)
//    {
//        this.fixtureWithAspDI = fixtureWithAspDI;
//    }

//    [Fact]
//    public void Test1()
//    {
//        var request = new Mock<CreateGeneralProfileRequest>();
//        fixtureWithAspDI.service.Create(request.Object);
//    }

//    [Fact]
//    public void Test2()
//    {
//        var reques2 = new Mock<CreateGeneralProfileRequest>();
//        fixtureWithAspDI.service.Create(reques2.Object);
//    }

//}

////We can also resolve dependencies using the the build-in IoC container
//public class GeneralProfileServiceFixtureWithAspDI : IDisposable
//{
//    private ServiceProvider _serviceProvider;

//    public IGeneralProfileRepository repository => _serviceProvider.GetRequiredService<IGeneralProfileRepository>();
//    public IGeneralProfileService service => _serviceProvider.GetRequiredService<IGeneralProfileService>();

//    public GeneralProfileServiceFixtureWithAspDI()
//    {
//        var service = new ServiceCollection();
//        service.AddScoped<IGeneralProfileRepository, GeneralProfileRepository>();
//        service.AddScoped<IGeneralProfileService, GeneralProfileService>();

//        _serviceProvider = service.BuildServiceProvider();
//    }

//    public void Dispose()
//    {
//        throw new NotImplementedException();
//    }
//}
