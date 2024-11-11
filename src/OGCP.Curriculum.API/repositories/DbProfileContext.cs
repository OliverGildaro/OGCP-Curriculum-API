using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.repositories
{
    public class DbProfileContext : DbContext
    {
        public DbProfileContext(DbContextOptions<DbProfileContext> dbContext)
            :base(dbContext)
        {
                
        }
        public DbSet<QualifiedProfile> QualifiedProfiles { get; set; }
        public DbSet<GeneralProfile> GeneralProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
    }
}
