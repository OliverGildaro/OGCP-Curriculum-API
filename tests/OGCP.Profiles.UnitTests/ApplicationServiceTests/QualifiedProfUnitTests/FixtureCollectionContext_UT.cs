using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.dtos;

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
        var result = await context.service.Create(request);

        Assert.IsType<int>(result);
        Assert.Equal(1, result);
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
        Add(QualifiedProfile.Create("Oliver", "CAstro", "I am bla", "Backedn").Value);
    }
}
