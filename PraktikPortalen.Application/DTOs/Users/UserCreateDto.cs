namespace PraktikPortalen.Application.DTOs.Users
{
    public sealed class UserCreateDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int Role { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }
}
