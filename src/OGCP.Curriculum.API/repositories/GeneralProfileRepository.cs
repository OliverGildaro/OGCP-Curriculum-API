using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories
{
    public class GeneralProfileRepository : GenericRepository<GeneralProfile, int>, IGeneralProfileRepository
    {
        public GeneralProfileRepository(DbProfileContext context)
            :base(context)
        {
            
        }
    }
}
