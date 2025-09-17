using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Domain.Interfaces.Repositories
{
    public interface IInternshipRepository
    {
        Task<List<Internship>> GetAllAsync(CancellationToken ct = default);
        Task<Internship?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Internship entity, CancellationToken ct = default);
        Task UpdateAsync(Internship entity, CancellationToken ct = default);
        Task DeleteAsync(Internship entity, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
    }
}
