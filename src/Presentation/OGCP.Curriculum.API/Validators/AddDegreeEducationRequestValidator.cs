using FluentValidation;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.Validators;

public class AddDegreeEducationRequestValidator
    : AbstractValidator<AddDegreeEducationRequest>
{
    public AddDegreeEducationRequestValidator()
    {
        RuleFor(x => x.Institution)
            .NotEmpty();

        RuleFor(x => x.Degree)
            .NotEmpty();
    }
}
