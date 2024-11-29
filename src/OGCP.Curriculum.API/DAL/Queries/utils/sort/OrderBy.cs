using System.Linq.Expressions;

using OGCP.Curriculum.API.DAL.Queries.Models;

public interface IOrderBy
{
    dynamic Expression { get; }
}

public class OrderBy<T> : IOrderBy
{
    private readonly Expression<Func<ProfileReadModel, T>> expression;

    public OrderBy(Expression<Func<ProfileReadModel, T>> expression)
    {
        this.expression = expression;
    }

    public dynamic Expression => this.expression;
}

