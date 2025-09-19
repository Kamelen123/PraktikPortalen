using FluentValidation;
using PraktikPortalen.Application.DTOs.Applications;

namespace PraktikPortalen.Application.Validation.Applications
{
    public class InternshipApplicationCreateDtoValidator : AbstractValidator<InternshipApplicationCreateDto>
    {
        public InternshipApplicationCreateDtoValidator()
        {
            RuleFor(x => x.InternshipId).GreaterThan(0);
            RuleFor(x => x.ApplicantId).GreaterThan(0);

            RuleFor(x => x.CoverLetter)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrWhiteSpace(x.CoverLetter));

            RuleFor(x => x.CvUrl)
                .MaximumLength(256)
                /*.Must(BeValidUrl).WithMessage("CvUrl must be a valid absolute URL.")*/
                .When(x => !string.IsNullOrWhiteSpace(x.CvUrl));
        }

        private static bool BeValidUrl(string? url) =>
            Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
