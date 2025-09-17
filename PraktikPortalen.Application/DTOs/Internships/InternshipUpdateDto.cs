using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.DTOs.Internships
{
    public sealed class InternshipUpdateDto
    {
        public string Title { get; set; } = default!;
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public LocationType LocationType { get; set; }
        public string? City { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public bool IsOpen { get; set; }
        public string? Description { get; set; }
    }
}
