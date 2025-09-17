using System.ComponentModel.DataAnnotations;

namespace PraktikPortalen.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; } = default!; // Unique (enforce in Db config)

        [MaxLength(256)]
        public string? Website { get; set; }

        [MaxLength(128)]
        public string? City { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
