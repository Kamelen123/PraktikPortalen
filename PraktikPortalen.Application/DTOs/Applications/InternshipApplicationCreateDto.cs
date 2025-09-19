namespace PraktikPortalen.Application.DTOs.Applications
{
    public sealed class InternshipApplicationCreateDto
    {
        public int InternshipId { get; set; }
        public int ApplicantId { get; set; }   // you can replace with current user later
        public string? CoverLetter { get; set; }
        public string? CvUrl { get; set; }
    }
}
