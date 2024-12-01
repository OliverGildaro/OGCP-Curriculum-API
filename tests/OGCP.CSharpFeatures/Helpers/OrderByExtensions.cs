namespace OGCP.CSharpFeatures.Helpers;

public static class OrderByExtensions
{
    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderBy(source, orderBy.Expression);
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, IOrderBy orderBy)
    {
        return Queryable.OrderByDescending(source, orderBy.Expression);
    }
}
