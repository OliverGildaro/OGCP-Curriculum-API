﻿using ArtForAll.Shared.ErrorHandler;
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
    public static TheoryData<GeneralProfile> Example_WithMethod()//since is static can be shared across diferent test clases
    {
        return new TheoryData<GeneralProfile>
        {
            GeneralProfile.Create("Oliver", "Castro", "Fullstack sumary", new string[] { "Be the best", "Another" }).Value,
            GeneralProfile.Create("Cristian", "Morato", "Fullstack sumary", new string[] { "Be the best", "Another" }).Value,
        };
    }

    [Theory]
    [MemberData(nameof(Example_WithMethod))]//this member data can be share across many unit tests
    public async Task Test1(GeneralProfile generalProfile)
    {
        var result = await fixture.service.Create(generalProfile);

        Assert.Equal(1, result);
        Assert.IsType<int>(result);
    }

    //here I have not been able to use fixture class context
    [Fact]
    public async Task Test2()
    {
        var mockRepo = new Mock<IGeneralProfileRepository>();

        mockRepo
              .Setup(m => m.Find())
              .ReturnsAsync(new List<GeneralProfile>()
              {
                        GeneralProfile.Create("Oliver", "Castro", "Fullstack dev", new string[]{ "goal"}).Value,
                        GeneralProfile.Create("Cristian", "Morato", "Fullstack dev senior", new string[]{ "goal"}).Value
              });

        var service = new GeneralProfileService(mockRepo.Object);

        var profiles = await service.Get();

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
            .ReturnsAsync(() => 1);

        repository
            .Setup(m => m.Find())
            .ReturnsAsync(new List<GeneralProfile>()
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