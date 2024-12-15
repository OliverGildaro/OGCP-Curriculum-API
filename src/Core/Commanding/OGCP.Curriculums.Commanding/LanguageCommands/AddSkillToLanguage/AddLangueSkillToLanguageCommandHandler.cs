using ArtForAll.Shared.Contracts.CQRS;
using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculums.Core.DomainModel.valueObjects;

namespace OGCP.Curriculum.API.commanding.commands.AddSkillToLanguage;

public class AddLangueSkillToLanguageCommandHandler : ICommandHandler<AddLangueSkillToLanguageCommand, Result>
{
    private readonly IProfileService profileService;

    public AddLangueSkillToLanguageCommandHandler(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    public async Task<Result> HandleAsync(AddLangueSkillToLanguageCommand command)
    {
        var skill = LanguageSkill.CreateNew(command.Skill, command.Level);
        if (skill.IsFailure)
        {
            return Result.Failure("");
        }

        return await profileService.AddLanguageSkillAsync(
            command.ProfileId, command.EducationId, skill.Value);
    }
}
