﻿using FluentValidation;
using OGCP.Curriculum.API.POCOS.requests.Profile;

namespace OGCP.Curriculum.API.Validators;

public class CreateStudentProfileRequestValidator
    : AbstractValidator<CreateStudentProfileRequest>
{
    public CreateStudentProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();
    }
}