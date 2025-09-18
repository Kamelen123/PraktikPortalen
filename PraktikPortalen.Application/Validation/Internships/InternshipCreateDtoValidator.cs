using FluentValidation;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.Validation.Internships
{
    public class InternshipCreateDtoValidator : AbstractValidator<InternshipCreateDto>
    {
        public InternshipCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().MaximumLength(160);

            RuleFor(x => x.CompanyId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);

            RuleFor(x => x.LocationType)
                .IsInEnum()
                .NotEqual(LocationType.Unassigned);

            RuleFor(x => x.City)
                .MaximumLength(128)
                .When(x => !string.IsNullOrWhiteSpace(x.City));

            RuleFor(x => x.ApplicationDeadline)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Application deadline must be in the future when creating an open internship.")
                .When(x => x.IsOpen);

            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}