using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculums.Core.DomainModel.Images;

namespace OGCP.Curriculums.Commanding.ProfileCommandImages.AddImage;
public class AddImageCommandHandler : ICommandHandler<AddImageCommand, Result>
{
    private readonly IProfileService profileService;

    public AddImageCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<Result> HandleAsync(AddImageCommand command)
    {

        //imageCreated
        var imageResult = Image.CreateNew(command.ProfileId, command.ContentType, command.FileName);
        if (imageResult.IsFailure)
        {
            return Result.Failure(imageResult.Error.Message);
        }


        return await profileService
            .AddImageAsync(command.ProfileId, imageResult.Value, command.ImageContent);
    }
}