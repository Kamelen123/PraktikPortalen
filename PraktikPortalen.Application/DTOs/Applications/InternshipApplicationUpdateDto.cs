using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.DTOs.Applications
{
    public sealed class InternshipApplicationUpdateDto
    {
        public ApplicationStatus Status { get; set; }
        public string? CoverLetter { get; set; }
        public string? CvUrl { get; set; }
    }
}
