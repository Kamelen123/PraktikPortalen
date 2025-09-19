namespace PraktikPortalen.Application.DTOs.Applications
{
    public sealed class InternshipApplicationDetailDto
    {
        public int Id { get; set; }
        public int InternshipId { get; set; }
        public string InternshipTitle { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public int ApplicantId { get; set; }
        public string ApplicantEmail { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string? CoverLetter { get; set; }
        public string? CvUrl { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
