using FluentValidation;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.Validation.Internships
{
    public class InternshipUpdateDtoValidator : AbstractValidator<InternshipUpdateDto>
    {
        public InternshipUpdateDtoValidator()
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

            // If IsOpen is true, enforce deadline in the future.
            RuleFor(x => x)
                .Must(x => !x.IsOpen || x.ApplicationDeadline > DateTime.UtcNow)
                .WithMessage("Cannot set IsOpen=true with a past deadline.");

            RuleFor(x => x.Description)
                .MaximumLength(2000);
        }
    }
}
