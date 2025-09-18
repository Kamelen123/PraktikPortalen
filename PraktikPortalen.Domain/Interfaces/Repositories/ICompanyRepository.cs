using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllAsync(CancellationToken ct = default);
        Task<Company?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
        Task AddAsync(Company entity, CancellationToken ct = default);
        Task UpdateAsync(Company entity, CancellationToken ct = default);
        Task DeleteAsync(Company entity, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
