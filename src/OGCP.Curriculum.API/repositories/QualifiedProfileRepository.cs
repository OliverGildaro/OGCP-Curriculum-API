using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories
{
    public class QualifiedProfileRepository : GenericRepository<QualifiedProfile, int>, IQualifiedProfileRepository
    {
        public QualifiedProfileRepository(DbProfileContext context)
            :base(context)
        {
            
        }
    }
}
