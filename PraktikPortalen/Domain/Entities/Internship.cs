using PraktikPortalen.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PraktikPortalen.Domain.Entities
{
    public class Internship
    {
        public int Id { get; set; }

        [Required, MaxLength(160)]
        public string Title { get; set; } = default!;

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public LocationType LocationType { get; set; } = LocationType.OnSite;

        [MaxLength(128)]
        public string? City { get; set; }

        [Required]
        public DateTime ApplicationDeadline { get; set; }

        public bool IsOpen { get; set; } = true;

        [MaxLength(2000)]
        public string? Description { get; set; }

        // Navigation
        public Company Company { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public ICollection<InternshipApplication> InternshipApplications { get; set; } = new List<InternshipApplication>();
    }
}
