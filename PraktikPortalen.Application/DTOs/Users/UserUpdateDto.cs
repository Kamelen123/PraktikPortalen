namespace PraktikPortalen.Application.DTOs.Users
{
    public sealed class UserUpdateDto
    {
        public string FullName { get; set; } = default!;
        public int Role { get; set; }
        public bool IsActive { get; set; }
    }
}
