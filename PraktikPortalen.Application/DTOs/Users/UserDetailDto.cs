namespace PraktikPortalen.Application.DTOs.Users
{
    public sealed class UserDetailDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
