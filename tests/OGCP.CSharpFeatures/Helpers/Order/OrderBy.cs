using OGCP.Curriculum.API.domainmodel;
using System.Linq.Expressions;

namespace OGCP.CSharpFeatures.Helpers.Order;

public class OrderBy<T> : IOrderBy
{
    private Expression<Func<QualifiedProfile, T>> value;

    public OrderBy(Expression<Func<QualifiedProfile, T>> value)
    {
        this.value = value;
    }

    public dynamic Expression => value;
}
