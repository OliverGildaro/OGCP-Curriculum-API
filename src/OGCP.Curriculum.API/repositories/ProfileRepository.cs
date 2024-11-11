using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(DbContext context)
            :base(context)
        {
            
        }
    }
}
