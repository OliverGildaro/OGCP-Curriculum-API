﻿using ArtForAll.Shared.ErrorHandler;
using Moq;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Profiles.UnitTests.serviceTests.GeneralProfUnitTests;

//***** CLASS DATA *****//////
//***** FIXTURE COLLECTION CONTEXT *****//////

[Collection("GeneralProfileServiceCollection")]
//Just decorating with the collection fixture will ensure that the test context will be provided here
public class FixtureCollectionContext_UT
{
    private readonly GeneralProfileServiceFixtureClass fixture;

    public FixtureCollectionContext_UT(GeneralProfileServiceFixtureClass fixture)
    {
        this.fixture = fixture;
    }

    [Theory]
    [ClassData(typeof(CreateGeneralProfileRequestTestData))]
    public async Task Test1(GeneralProfile generalProfile)
    {
        var result = await fixture.service.Create(generalProfile);

        Assert.Equal(1, result);
        Assert.IsType<int>(result);
    }


}

//We are wrapping the fixture class aproach
//FIXTURE COLLECTION TEST CONTEXT
[CollectionDefinition("GeneralProfileServiceCollection")]
public class GeneralProfileServiceCollectionFixture
    : ICollectionFixture<GeneralProfileServiceFixtureClass>
{

}

//***** CLASS DATA *****//////
public class CreateGeneralProfileRequestTestData : TheoryData<GeneralProfile>
{
    public CreateGeneralProfileRequestTestData()
    {
        Add(
            GeneralProfile.Create("Oliver", "Castro", "Fullstack sumary", new string[] { "Be the best", "Another" }).Value);
    }
}