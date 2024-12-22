using OGCP.Curriculums.Core.DomainModel.interfaces;

namespace OGCP.Curriculums.Core.DomainModel.Events;
public class ImageAdded : IDomainEvent
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string CreatedAt { get; set; }
    public string contentType { get; set; }
    public string fileName { get; set; }

    public async Task Accept()
    {
    }
}