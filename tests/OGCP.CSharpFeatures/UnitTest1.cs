using System.Linq.Expressions;

namespace OGCP.CSharpFeatures;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //lets build the x > 1 expression
        var xExpression = Expression.Parameter(typeof(int), "x");
        var constantExpression = Expression.Constant(12);
        var greaterThan = Expression.GreaterThan(xExpression, constantExpression);

        var constant4Expression = Expression.Constant(4);
        var lessThan = Expression.GreaterThan(xExpression, constant4Expression);

        var or = Expression.Or(greaterThan, lessThan);

        //We need to pass the xExpression twice because the lambda needs its own argument to match the expression
        //So we can satisfied the Func<int, bool> signature
        var funcExpr = Expression.Lambda<Func<int, bool>>(or, false, new List<ParameterExpression> { xExpression });
        var func = funcExpr.Compile();

        Assert.False(func(0));
        Assert.True(func(5));
    }
}