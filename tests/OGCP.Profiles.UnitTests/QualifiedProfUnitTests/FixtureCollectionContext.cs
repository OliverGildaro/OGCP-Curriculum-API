using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.dtos;

namespace OGCP.Profiles.UnitTests.QualifiedProfUnitTests;

[Collection("QualifiedProfileCollection")]
public class FixtureCollectionContext : IDisposable
{
    private readonly QualifiedProfileContext context;

    public FixtureCollectionContext(QualifiedProfileContext context)
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

[CollectionDefinition("QualifiedProfileCollection")]
public class QualifiedProfileCollectionFixture : ICollectionFixture<QualifiedProfileContext>
{
}

public class CreateQualifiedProfileRequestClassData
    :TheoryData<CreateQualifiedProfileRequest>
{
    public CreateQualifiedProfileRequestClassData()
    {
        this.Add(new CreateQualifiedProfileRequest
        {
            FirstName = "Oliver",
            LastName = "Castro",
            Summary = "I am bla",
            DesiredJobRole = "Backend"
        });
    }
}
