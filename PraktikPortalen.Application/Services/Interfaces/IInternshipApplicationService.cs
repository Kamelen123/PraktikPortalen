using PraktikPortalen.Application.DTOs.Applications;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface IInternshipApplicationService
    {
        Task<List<InternshipApplicationListDto>> GetAllAsync(CancellationToken ct = default);
        Task<List<InternshipApplicationListDto>> GetByApplicantAsync(int applicantId, CancellationToken ct = default);
        Task<InternshipApplicationDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<int> CreateAsync(InternshipApplicationCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, InternshipApplicationUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
