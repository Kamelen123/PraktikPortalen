namespace PraktikPortalen.Application.DTOs.Users
{
    public sealed class UserListDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Role { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
