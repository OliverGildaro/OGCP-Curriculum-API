using Microsoft.EntityFrameworkCore.Design;
using OGCP.Curriculum.API.DAL.Mutations.context;
using OGCP.Curriculum.API.DAL.Queries.context;

namespace OGCP.Curriculum.API.Helpers.DIMounters
{
    public static class DbContextMounter
    {
        public static void SetupDbContext(this IServiceCollection Services, IConfiguration Configuration)
        {

            // Add services to the container.
            //The dbcontext is automatically dispose after getting out of scope along with the tracking objects
            //Services.AddScoped<ApplicationWriteDbContext>();
            //{
                //I need to register in this way because there is an abiguity between the two constructors I have
                //In the DbProfileContext
            //    var configuration = provider.GetRequiredService<DbProfileWritesContextConfig>();
            //    return new ApplicationWriteDbContext(new DbProfileWritesContextConfig
            //    {
            //        ConnectionString = Configuration.GetConnectionString("conectionDb"),
            //        UseConsoleLogger = true
            //    });
            //});

                //I need to register in this way because there is an abiguity between the two constructors I have
                //In the DbProfileContext
            //Services.AddScoped<ApplicationReadDbContext>(provider =>
            //{
            //    return new ApplicationReadDbContext(new DbProfileReadsContextConfig
            //    {
            //        ConnectionString = Configuration.GetConnectionString("conectionDb"),
            //        UseConsoleLogger = true
            //    });
            //});
        }
    }
}

