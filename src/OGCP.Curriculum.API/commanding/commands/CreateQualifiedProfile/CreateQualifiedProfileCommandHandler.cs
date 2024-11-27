using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Results;
using Azure.Core;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile
{
    public class CreateQualifiedProfileCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : CreateProfileCommand
        where TResult : Result
    {
        private readonly IProfileService profileService;

        public CreateQualifiedProfileCommandHandler(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public async Task<TResult> HandleAsync(TCommand command)
        {
            IResult<Profile, Error> profileResult = command.MapTo();
            var profile = profileResult.Value;

            Result addEducationResult = await this.profileService.Create(profile);

            return (TResult)addEducationResult;
        }
        //public async Task<Result> HandleAsync(CreateQualifiedProfileCommand command)
        //{
        //    (string firstName, string lastName, string summary, string desiredJobRole) = command;
        //    var qualified = QualifiedProfile.Create(firstName, lastName, summary, desiredJobRole);
        //    var asa = await profileService.Create(qualified.Value);

        //    return Result.Success();
        //}
    }
}
