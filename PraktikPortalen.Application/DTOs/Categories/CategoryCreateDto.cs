namespace PraktikPortalen.Application.DTOs.Categories
{
    public sealed class CategoryCreateDto
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
