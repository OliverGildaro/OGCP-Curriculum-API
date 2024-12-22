using Microsoft.Extensions.Azure;

namespace OGCP.Curriculum.API.Helpers.DIMounters;

public static class AzzureServicesMounter
{
    public static void SetupAzureServices(this IServiceCollection Services, IConfiguration Configuration)
    {
        Services.AddAzureClients(azureBuilder =>
        {
            azureBuilder.AddBlobServiceClient(Configuration.GetConnectionString("blobStorage"));
        });
    }
}
