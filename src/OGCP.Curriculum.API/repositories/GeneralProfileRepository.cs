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
    }
}
