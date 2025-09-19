namespace PraktikPortalen.Application.DTOs.Applications
{
    public sealed class InternshipApplicationListDto
    {
        public int Id { get; set; }
        public string InternshipTitle { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime SubmittedAt { get; set; }
    }
}
