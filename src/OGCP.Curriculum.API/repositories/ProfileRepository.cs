using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories.interfaces;

namespace OGCP.Curriculum.API.repositories
{
    public class ProfileRepository : GenericRepository<Profile, int>, IProfileRepository
    {
        public ProfileRepository(DbProfileContext context) : base(context)
        {
        }
    }
}
