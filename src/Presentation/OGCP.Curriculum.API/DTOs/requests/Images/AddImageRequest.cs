namespace OGCP.Curriculum.API.DTOs.requests.Images;
    public record ImageAddRequest(
        int profileId,
        IFormFile? Image);
