using OGCP.CSharpFeatures.Helpers.Order;
using System.Dynamic;

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

    [Fact]
    public void Expanding1()
    {
        var fakeRepo = new FakeRepo();
        IEnumerable<ExpandoObject> result = fakeRepo.Find(new string[] { "FirstName" });

        // Assert the results
        var expectedResults = new[]
        {
        new { FirstName = "Oliver", LastName = "Castro" },
        new { FirstName = "Alvaro", LastName = "Castro" },
        new { FirstName = "Ivan", LastName = "Castro" }
    };

        int index = 0;
        foreach (var expando in result)
        {
            // Cast the ExpandoObject to a dictionary to access its properties
            var dictionary = expando as IDictionary<string, object>;

            // Assert the FirstName and LastName values
            Assert.True(dictionary.ContainsKey("FirstName"));
            Assert.Equal(expectedResults[index].FirstName, dictionary["FirstName"]);

            Assert.False(dictionary.ContainsKey("LastName"));
            //Assert.Equal(expectedResults[index].LastName, dictionary["LastName"]);

            Assert.False(dictionary.ContainsKey("Summary"));

            index++;
        }
    }
}
