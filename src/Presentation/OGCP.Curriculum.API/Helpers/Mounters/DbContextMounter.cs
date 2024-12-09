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
            //builder.Services.AddScoped(provider => new DbProfileContextConfig
            //{
            //    ConnectionString = builder.Configuration.GetConnectionString("conectionDb"),
            //    UseConsoleLogger = true
            //});
            Services.AddScoped<ApplicationWriteDbContext>(provider =>
            {
                //I need to register in this way because there is an abiguity between the two constructors I have
                //In the DbProfileContext
                return new ApplicationWriteDbContext(new DbProfileWritesContextConfig
                {
                    ConnectionString = Configuration.GetConnectionString("conectionDb"),
                    UseConsoleLogger = true
                });
            });

            Services.AddScoped<ApplicationReadDbContext>(provider =>
            {
                //I need to register in this way because there is an abiguity between the two constructors I have
                //In the DbProfileContext
                return new ApplicationReadDbContext(new DbProfileReadsContextConfig
                {
                    ConnectionString = Configuration.GetConnectionString("conectionDb"),
                    UseConsoleLogger = true
                });
            });
        }
    }
}
