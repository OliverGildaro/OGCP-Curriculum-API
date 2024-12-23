using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculums.Core.DomainModel.Images;

public class ImageBuffer
{
    private int _id;
    private byte[] content;
    private string key;

    private ImageBuffer(byte[] content, int imageId)
    {
        this._id = imageId;
        this.content = content;
        this.key = $"images/{imageId}";
    }
    public int Id => -Id;
    public string Key => key;
    public byte[] Content => content;
    public static Result<ImageBuffer, Error> CreateNew(byte[] content, int imageId)
    {
        return new ImageBuffer(content, imageId);
    }
}

