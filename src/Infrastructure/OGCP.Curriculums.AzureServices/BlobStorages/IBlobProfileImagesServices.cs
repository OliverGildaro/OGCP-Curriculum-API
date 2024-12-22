using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculums.Core.DomainModel.Images;

namespace OGCP.Curriculums.AzureServices.BlobStorages;

public interface IBlobProfileImagesServices
{
    Task<Result> UploadImageAsync(ImageBuffer imageFile);
}
