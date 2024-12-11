using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;
using OGCP.Curriculums.Core.DomainModel.profiles;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Profiles.UnitTests.serviceTests.QualifiedProfUnitTests;

[Collection("QualifiedProfileServiceCollection")]
public class FixtureCollectionContext_UT : IDisposable
{
    private readonly QualifiedProfileServiceFIxtureClass context;

    public FixtureCollectionContext_UT(QualifiedProfileServiceFIxtureClass context)
    {
        this.context = context;
    }

    [Theory]
    [ClassData(typeof(CreateQualifiedProfileRequestClassData))]
    public async Task test1(QualifiedProfile request)
    {
        var result = await context.service.CreateAsync(request);

        Assert.IsType<Result>(result);
    }

    public void Dispose()
    {
    }
}

[CollectionDefinition("QualifiedProfileServiceCollection")]
public class QualifiedProfileServiceCollectionFixture : ICollectionFixture<QualifiedProfileServiceFIxtureClass>
{
}

public class CreateQualifiedProfileRequestClassData
    : TheoryData<QualifiedProfile>
{
    public CreateQualifiedProfileRequestClassData()
    {
        Add(QualifiedProfile.Create(
            Name.CreateNew("Oliver", "Castro").Value,
            "I am bla",
            "Backedn",
            PhoneNumber.CreateNew("591", "69554851").Value,
            Email.CreateNew("gildaro.castro@gmai.com").Value).Value);
    }
}
