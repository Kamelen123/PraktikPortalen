using System.ComponentModel.DataAnnotations;

namespace PraktikPortalen.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; } = default!; // Unique (enforce in Db config)

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Internship> Internships { get; set; } = new List<Internship>();
    }
}
