namespace PraktikPortalen.Application.DTOs.Auth
{
    public sealed class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTime ExpiresAtUtc { get; set; }
    }
}
