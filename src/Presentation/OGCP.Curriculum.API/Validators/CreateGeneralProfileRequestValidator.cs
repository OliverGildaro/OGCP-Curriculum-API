using FluentValidation;
using OGCP.Curriculum.API.POCOS.requests.Profile;

namespace OGCP.Curriculum.API.Validators;

public class CreateGeneralProfileRequestValidator
    : AbstractValidator<CreateGeneralProfileRequest>
{
    public CreateGeneralProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();
    }
}
