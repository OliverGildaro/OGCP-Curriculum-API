using ArtForAll.Shared.ErrorHandler;
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
    public void test1(CreateQualifiedProfileRequest request)
    {
        var result = context.service.Create(request);

        Assert.IsType<Result>(result);
        Assert.NotNull(result);
        Assert.True(result.IsSucces);
        Assert.Empty(result.Message);
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
    : TheoryData<CreateQualifiedProfileRequest>
{
    public CreateQualifiedProfileRequestClassData()
    {
        Add(new CreateQualifiedProfileRequest
        {
            FirstName = "Oliver",
            LastName = "Castro",
            Summary = "I am bla",
            DesiredJobRole = "Backend"
        });
    }
}
