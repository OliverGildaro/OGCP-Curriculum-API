using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using OGCP.Curriculums.AzureServices.BlobStorages;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class AzureServicesMounter
{
    public static void SetupAzureServices(this IServiceCollection Services, IConfiguration Configuration)
    {
        SetupBlobStorage(Services, Configuration);
        SetupADB2C(Services, Configuration);   
    }

    public static void SetupBlobStorage(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddScoped<IBlobProfileImagesServices, BlobProfileImagesServices>();
        Services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(Configuration.GetConnectionString("blobStorage"));
        });
    }

    public static void SetupADB2C(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));

        //var initialScopes = Configuration["AzureAdB2C:Scopes"]?.Split(' ');
        Services.AddAuthorization();
    }

}
