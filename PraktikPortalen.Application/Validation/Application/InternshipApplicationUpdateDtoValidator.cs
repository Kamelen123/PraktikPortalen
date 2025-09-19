using FluentValidation;
using PraktikPortalen.Application.DTOs.Applications;
using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.Validation.Applications
{
    public class InternshipApplicationUpdateDtoValidator : AbstractValidator<InternshipApplicationUpdateDto>
    {
        public InternshipApplicationUpdateDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .NotEqual(ApplicationStatus.Unassigned);

            RuleFor(x => x.CoverLetter)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrWhiteSpace(x.CoverLetter));

            RuleFor(x => x.CvUrl)
                .MaximumLength(256)
                /*.Must(BeValidUrl).WithMessage("CvUrl must be a valid absolute URL.")*/
                .When(x => !string.IsNullOrWhiteSpace(x.CvUrl));
        }

        /*private static bool BeValidUrl(string? url) =>
            Uri.TryCreate(url, UriKind.Absolute, out _);*/
    }
}
