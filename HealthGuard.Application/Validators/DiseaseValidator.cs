using FluentValidation;
using HealthGuard.Application.DTOs.Disease;

namespace HealthGuard.Application.Validators
{
    public class DiseaseValidator : AbstractValidator<CreateDiseaseDto>
    {
        public DiseaseValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(r => r.Description)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(r => r.VaccineAvailable)
                .NotNull();

            RuleFor(r => r.Symptoms)
                .NotEmpty();
        }
    }
}
