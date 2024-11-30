using System.Linq.Expressions;

using OGCP.Curriculum.API.DAL.Queries.Models;

public interface IOrderBy
{
    dynamic Expression { get; }
}

//The expression three is independent from the provider
//Providers can be LinQ to EF, LinQ to XML, LinQ to Objects
//These tree providers have equivalent schemas, have equivalent queries
//You can use the same expression tree to navigate trougth
//The expression tree is universal
//Using lambda function we are building expression trees
//But the functionality is difined in compile time
//Using expression trees the user application can decide what kind of code wants to build
public class OrderBy<T> : IOrderBy
{
    private readonly Expression<Func<ProfileReadModel, T>> expression;

    public OrderBy(Expression<Func<ProfileReadModel, T>> expression)
    {
        this.expression = expression;
    }

    public dynamic Expression => this.expression;
}

