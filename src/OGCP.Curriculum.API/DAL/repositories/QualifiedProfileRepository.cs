using ArtForAll.Shared.ErrorHandler;
using Microsoft.EntityFrameworkCore;
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
            //Examples that what I can do
            //QUERY PROJECTIONS
            //1. I can query some properties from database (Expand feature)
            //The dbCOntext will track only properties that is able to recognize
            //base.context.profile.Select(p => new
            //{
            //  FirstName = p.FirstName,
            //  LastName=p.LastName,
            //  LanguagesCount = p.Languages.Where(lang => lang.Name.Equals("Spanish")).Count()
            //})
            //2.EAGER LOADING
            //I can include nested children
            //base.context.profiles.Include(p => p.Educations).ThenInclude(e => e.Institutions);
            //3. EXPLICIT LOADING
            //I can query some data on an existing entity in dbcontext memory
            //First I load the entity
            //var profile = context.Profiles.FirstOrDefault(p => p.id==id)
            //Then I can load related data explicitly
            //contex.Entry(profile).Collection(p => p.Educations).Load();
            //I can append a query too
            //contex.Entry(profile).Collection(p => p.Educations)
            //.Query().Where(e => e.Institution.Contains("sdsd")).ToList();
            //4. LAZZY LOADING
            //AVOID!! First will get all educations from database then will count with collection in memory
            //var count = profiles.Educations.Count();
            //To enable you need to add Proxies library
            //5.DISCONNECTED
            //For a disconected graph avoid next because will mark as modified all objects in the graph:
            //var newContext = new DbContext()
            //newContex.Profiles.Update(profile.Educations[0])
            //Instead do this:
            //newContex.Entry(profile.Educations[0]).State = EntityState.Modified

            return base.Find(id, includes);
        }

        public async Task<Result> RemoveOrphanEducations(string removeEducation)
        {
            var isSaved = await this.context.Database.ExecuteSqlRawAsync(removeEducation);

            if (isSaved < 0)
            {
                return Result.Failure("");
            }
            return Result.Success();
        }
    }
}
