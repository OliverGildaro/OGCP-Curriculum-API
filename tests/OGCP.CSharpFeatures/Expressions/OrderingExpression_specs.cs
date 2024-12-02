using OGCP.Curriculum.API.DAL.Queries.utils;

namespace OGCP.CSharpFeatures.Expressions;

public class OrderingExpression_specs
{
    [Fact]
    public async Task Ordering1()
    {
        var fakeRepo = new FakeRepo();
        var parameters = new QueryParameters
        {
            OrderBy = "FirstName"
        };

        var result = await fakeRepo.Find(parameters);
        Assert.Equal("Alvaro", result[0].FirstName);
        Assert.Equal("Ivan", result[1].FirstName);
        Assert.Equal("Oliver", result[2].FirstName);
    }

    [Fact]
    public async Task Expanding()
    {
        var fakeRepo = new FakeRepo();
        var parameters = new QueryParameters
        {
            Fields = "FirstName, LastName"
        };

        var result = await fakeRepo.Find(parameters);
        Assert.Equal("Oliver", result[0].FirstName);
        Assert.Equal("Alvaro", result[1].FirstName);
        Assert.Equal("Ivan", result[2].FirstName);
    }
}
