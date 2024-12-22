using ArtForAll.Shared.ErrorHandler;

namespace OGCP.Curriculums.Core.DomainModel.Images;

public class Image
{
    private int _id;
    private int profileId;
    private string contentType;
    private string fileName;

    protected Image()
    {

    }

    private Image(int profileId, string contentType, string fileName)
    {
        this.profileId = profileId;
        this.contentType = contentType;
        this.fileName = fileName;
    }

    public string ContentType => contentType;
    public string FileName => fileName;
    public int ProfileId => profileId;
    public int Id => _id;
    public static Result<Image, Error> CreateNew(int profileId, string contentType, string fileName)
    {
        return new Image(profileId, contentType, fileName);
    }

    internal void Update(string contentType, string fileName)
    {
        this.contentType = contentType;
        this.fileName = fileName;
    }
}
