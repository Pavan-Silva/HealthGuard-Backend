﻿using FluentValidation;
using HealthGuard.Application.DTOs.Disease;

namespace HealthGuard.Application.Validators
{
    public class SymptomValidator : AbstractValidator<SymptomRequest>
    {
        public SymptomValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(r => r.Description)
                .NotEmpty()
                .MinimumLength(10);
        }
    }
}