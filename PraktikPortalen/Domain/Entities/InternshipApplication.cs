using PraktikPortalen.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PraktikPortalen.Domain.Entities
{
    public class InternshipApplication
    {
        public int Id { get; set; }

        [Required]
        public int InternshipId { get; set; }

        [Required]
        public int ApplicantId { get; set; } // FK -> User

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;

        [MaxLength(4000)]
        public string? CoverLetter { get; set; }

        [MaxLength(512)]
        public string? CvUrl { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Internship Internship { get; set; } = default!;
        public User Applicant { get; set; } = default!;
    }
}
