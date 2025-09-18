namespace PraktikPortalen.Application.DTOs.Companies
{
    public sealed class CompanyUpdateDto
    {
        public string Name { get; set; } = default!;
        public string? Website { get; set; }
        public string? City { get; set; }
        public bool IsActive { get; set; }
    }
}
