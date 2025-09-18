using FluentValidation;
using PraktikPortalen.Application.DTOs.Companies;

namespace PraktikPortalen.Application.Validation.Companies
{
    public class CompanyCreateDtoValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(128);

            RuleFor(x => x.Website)
                .MaximumLength(256)
                .When(x => !string.IsNullOrWhiteSpace(x.Website));

            RuleFor(x => x.City)
                .MaximumLength(128)
                .When(x => !string.IsNullOrWhiteSpace(x.City));

        }

    }
}
