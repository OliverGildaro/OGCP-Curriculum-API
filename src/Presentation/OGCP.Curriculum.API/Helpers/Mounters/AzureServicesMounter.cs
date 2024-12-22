using Microsoft.Extensions.Azure;
using OGCP.Curriculums.AzureServices.BlobStorages;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class AzureServicesMounter
{
    public static void SetupAzureServices(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddScoped<IBlobProfileImagesServices, BlobProfileImagesServices>();
        Services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(Configuration.GetConnectionString("blobStorage"));
        });
    }
}
