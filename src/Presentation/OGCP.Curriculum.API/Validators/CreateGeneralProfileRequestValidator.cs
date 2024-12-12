using FluentValidation;
using OGCP.Curriculum.API.POCOS.requests.Profile;
using OGCP.Curriculums.API.Validators;

namespace OGCP.Curriculum.API.Validators;

public class CreateGeneralProfileRequestValidator
    : AbstractValidator<CreateGeneralProfileRequest>
{
    public CreateGeneralProfileRequestValidator()
    {
        RuleFor(x => x.Name.GivenName)
            .NotEmptyCustom();

        RuleFor(x => x.Name.FamilyNames)
            .NotEmptyCustom();
    }
}
