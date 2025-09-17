namespace PraktikPortalen.Application.DTOs.Internships
{
    public sealed class InternshipListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string? City { get; set; }
        public bool IsOpen { get; set; }
        public DateTime ApplicationDeadline { get; set; }
    }
}
