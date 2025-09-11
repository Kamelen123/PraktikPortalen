namespace PraktikPortalen.Application.DTOs.Auth
{
    public sealed class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
