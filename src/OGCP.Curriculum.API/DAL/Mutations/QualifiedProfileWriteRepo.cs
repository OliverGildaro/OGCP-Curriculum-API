using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.EntityFrameworkCore;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.DAL.Mutations;

public class QualifiedProfileWriteRepo : ProfileWriteRepo, IQualifiedProfileWriteRepo
{
    private readonly DbProfileContext context;

    public QualifiedProfileWriteRepo(DbProfileContext context)
        :base(context)
    {
        this.context = context;
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

    public async Task<Maybe<QualifiedProfile>> Find(int id)
    {
        return await this.context.QualifiedProfiles
            .Include(p => p.LanguagesSpoken)
            .Include(p => p.Educations)
            .FirstOrDefaultAsync(p => p.Id.Equals(id));
    }

    public Task<int> SaveChanges()
    {
        return base.SaveChanges();
    }

    public Result Add(QualifiedProfile entity)
    {
        return base.Add(entity);
    }
}

//TO have this.context.Profiles.Update(profile); will update all fields of the entity
//even thou only one has changed
//SO to implement the Update method is not a good isea
//Update() is for disconnected entities, especial cases


//To delete an entity first we need to get the entity
//then this.context.Remove(entity);


//AddRange(entities) //This is a bulk sql operation
//emoveRange() and UpdateRange()


//THis is how can I execute stored PROCS
//var storeProc = "dsdsdsdsd dsdsds"
//I can use this.context.profiles.FromSQlRaw(storeProc);
//I can use this.context.profiles.FromSQlInterpolated(storeProc);
//I need to parameterized my sql scripts to protect agains sql injection
//I can build on the top .AsNoTracking() or .Include()
//And the result of the SP needs to match the materialized entity

//Is posible to create views independent form tables
//Are for readonly queries
//And you need to create a model entity for that
//We can create a view like we did with stored proc
//And setup in the OnModelCreating that the entity will comes from a view

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