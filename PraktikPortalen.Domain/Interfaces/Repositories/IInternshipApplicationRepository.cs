using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Domain.Interfaces.Repositories
{
    public interface IInternshipApplicationRepository
    {
        Task<List<InternshipApplication>> GetAllAsync(CancellationToken ct = default);
        Task<List<InternshipApplication>> GetByApplicantAsync(int applicantId, CancellationToken ct = default);
        Task<InternshipApplication?> GetByIdAsync(int id, CancellationToken ct = default);

        Task AddAsync(InternshipApplication entity, CancellationToken ct = default);
        Task UpdateAsync(InternshipApplication entity, CancellationToken ct = default);
        Task DeleteAsync(InternshipApplication entity, CancellationToken ct = default);

        Task<bool> SaveChangesAsync(CancellationToken ct = default);
        Task<bool> ExistsForApplicantAsync(int internshipId, int applicantId, CancellationToken ct = default);
    }
}
