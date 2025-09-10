using PraktikPortalen.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PraktikPortalen.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; } = default!;

        [Required]
        public string PasswordHash { get; set; } = default!;

        [Required, MaxLength(128)]
        public string FullName { get; set; } = default!;

        [Required]
        public UserRole Role { get; set; } = UserRole.Member;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<InternshipApplication> InternshipApplications { get; set; } = new List<InternshipApplication>();
    }
}
