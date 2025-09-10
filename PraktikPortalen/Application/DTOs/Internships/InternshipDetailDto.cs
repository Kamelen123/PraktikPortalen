namespace PraktikPortalen.Application.DTOs.Internships
{
    public sealed class InternshipDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string? City { get; set; }
        public string LocationType { get; set; } = default!;
        public bool IsOpen { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public string? Description { get; set; }
    }
}
