using FluentValidation;
using OGCP.Curriculum.API.POCOS.requests.Education;
using OGCP.Curriculums.API.Validators;

namespace OGCP.Curriculum.API.Validators;

public class AddDegreeEducationRequestValidator
    : AbstractValidator<AddDegreeEducationRequest>
{
    public AddDegreeEducationRequestValidator()
    {
        RuleFor(x => x.Institution)
            .LengthCustom(0, 100)
            .NotEmptyCustom();

        RuleFor(x => x.Degree)
            .NotEmptyCustom();
    }
}
