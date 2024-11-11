using Microsoft.EntityFrameworkCore;

namespace OGCP.Curriculum.API.repositories;

public abstract class Repository<T> : IRepository<T>
{
    private DbContext context;

    protected Repository(DbContext context)
    {
        this.context = context;
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public T FInd()
    {
        throw new NotImplementedException();
    }
}
