//using ArtForAll.Shared.Contracts.CQRS;
//using ArtForAll.Shared.ErrorHandler;
//using Azure.Core;
//using OGCP.Curriculum.API.domainmodel;
//using OGCP.Curriculum.API.services.interfaces;

//namespace OGCP.Curriculum.API.commanding.commands.CreateGeneralProfile
//{
//    public class CreateGeneralProfileCommandHandler : ICommandHandler<CreateGeneralProfileCommand, Result>
//    {
//        private readonly IGeneralProfileService profileService;

//        public CreateGeneralProfileCommandHandler(IGeneralProfileService profileService)
//        {
//            this.profileService = profileService;
//        }
//        public async Task<Result> HandleAsync(CreateGeneralProfileCommand command)
//        {
//            (string firstName, string lastName, string summary, string[] personalGoals) = command;
//            var generalProfile = GeneralProfile.Create(firstName, lastName, summary, personalGoals);
//            var asa = await profileService.Create(generalProfile.Value);

//            return Result.Success();
//        }
//    }
//}
