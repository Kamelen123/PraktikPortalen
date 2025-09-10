using PraktikPortalen.Application.DTOs.Internships;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface IInternshipService
    {
        Task<List<InternshipListDto>> GetAllAsync(CancellationToken ct = default);
        Task<InternshipDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(InternshipCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, InternshipUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
