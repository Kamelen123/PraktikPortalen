using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PraktikportalenDbContext _db;
        public CompanyRepository(PraktikportalenDbContext db) => _db = db;

        public Task<List<Company>> GetAllAsync(CancellationToken ct = default) =>
            _db.Companies.AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync(ct);

        public Task<Company?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Companies.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task AddAsync(Company entity, CancellationToken ct = default) =>
            await _db.Companies.AddAsync(entity, ct);

        public Task UpdateAsync(Company entity, CancellationToken ct = default)
        {
            _db.Companies.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Company entity, CancellationToken ct = default)
        {
            _db.Companies.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _db.SaveChangesAsync(ct) > 0;

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
            _db.Companies.AnyAsync(c => c.Id == id, ct);
    }
}
