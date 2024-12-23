using ArtForAll.Shared.ErrorHandler;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using OGCP.Curriculums.Core.DomainModel.Images;
using static System.Net.Mime.MediaTypeNames;

namespace OGCP.Curriculums.AzureServices.BlobStorages;

public class BlobProfileImagesServices  : IBlobProfileImagesServices
{
    private readonly BlobServiceClient client;

    public BlobProfileImagesServices(BlobServiceClient client)
    {
        this.client = client;
    }

    public async Task<Result> UploadImageAsync(ImageBuffer imageFile)
    {
        try
        {

            using var stream = new MemoryStream(imageFile.Content);
            var containerClient = this.client.GetBlobContainerClient("oliver");
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            string blobName = $"{Guid.NewGuid().ToString()}.jpg";

            var response = await containerClient.UploadBlobAsync(blobName, stream);

            return Result.Success("Sucess");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
