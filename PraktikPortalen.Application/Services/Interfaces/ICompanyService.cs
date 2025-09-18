using PraktikPortalen.Application.DTOs.Companies;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyListDto>> GetAllAsync(CancellationToken ct = default);
        Task<CompanyDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CompanyCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, CompanyUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
