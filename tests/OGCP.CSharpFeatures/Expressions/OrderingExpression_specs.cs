using OGCP.CSharpFeatures.Helpers;

namespace OGCP.CSharpFeatures.Expressions;

public class OrderingExpression_specs
{
    [Fact]
    public void Ordering1()
    {
        var fakeRepo = new FakeRepo();
        var result = fakeRepo.Find("FirstName").ToList();
        Assert.Equal("Alvaro", result[0].FirstName);
        Assert.Equal("Ivan", result[1].FirstName);
        Assert.Equal("Oliver", result[2].FirstName);
    }
}
