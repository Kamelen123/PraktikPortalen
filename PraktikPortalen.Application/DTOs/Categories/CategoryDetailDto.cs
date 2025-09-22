namespace PraktikPortalen.Application.DTOs.Categories
{
    public sealed class CategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
