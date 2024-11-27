//using ArtForAll.Shared.Contracts.CQRS;
//using ArtForAll.Shared.ErrorHandler;
//using OGCP.Curriculum.API.domainmodel;
//using OGCP.Curriculum.API.services.interfaces;

//namespace OGCP.Curriculum.API.commanding.commands.CreateStudentProfile;

//public class CreateStudentProfileCommandHandler : ICommandHandler<CreateStudentProfileCommand, Result>
//{
//    private readonly IStudentProfileService profileService;

//    public CreateStudentProfileCommandHandler(IStudentProfileService profileService)
//    {
//        this.profileService = profileService;
//    }

//    public async Task<Result> HandleAsync(CreateStudentProfileCommand command)
//    {
//        (string firstName, string lastName, string summary, string major, string careerGoals) = command;
//        var studentProfile = StudentProfile.Create(firstName, lastName, summary, major, careerGoals);
//        var asa = await profileService.Create(studentProfile.Value);

//        return Result.Success();
//    }
//}
