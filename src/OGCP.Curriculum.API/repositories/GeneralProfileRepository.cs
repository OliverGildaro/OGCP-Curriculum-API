using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories
{
    public class GeneralProfileRepository : Repository<Profile>, IGeneralProfileRepository
    {
        public GeneralProfileRepository(DbProfileContext context)
            :base(context)
        {
            
        }
    }
}
