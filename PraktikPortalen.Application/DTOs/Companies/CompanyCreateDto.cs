namespace PraktikPortalen.Application.DTOs.Companies
{
    public sealed class CompanyCreateDto
    {
        public string Name { get; set; } = default!;
        public string? Website { get; set; }
        public string? City { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
