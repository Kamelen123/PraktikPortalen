using PraktikPortalen.Domain.Enums;

namespace PraktikPortalen.Application.DTOs.Auth
{
    public sealed class RegisterDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public UserRole Role { get; set; } = UserRole.Member; // default Member
    }
}
