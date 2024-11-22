using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.repositories
{
    public class QualifiedProfileRepository : GenericRepository<QualifiedProfile, int>, IQualifiedProfileRepository
    {
        public QualifiedProfileRepository(DbProfileContext context)
            :base(context)
        {
            
        }

        public override Task<QualifiedProfile> Find(int id, params Expression<Func<QualifiedProfile, object>>[] includes)
        {
            return base.Find(id, includes);
        }
    }
}
