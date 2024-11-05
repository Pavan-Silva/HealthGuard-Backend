using FluentValidation;
using HealthGuard.Application.DTOs.Auth;

namespace HealthGuard.Application.Validators
{
    public class RegistrationValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegistrationValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(r => r.LastName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(r => r.PhoneNumber)
                .NotEmpty()
                .Length(10);
        }
    }
}
