using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Repositories
{
    public class InternshipRepository : IInternshipRepository
    {
        private readonly PraktikportalenDbContext _db;

        public InternshipRepository(PraktikportalenDbContext db) => _db = db;

        public Task<List<Internship>> GetAllAsync(CancellationToken ct = default) =>
            _db.Internships
               .AsNoTracking()
               .Include(i => i.Company)
               .Include(i => i.Category)
               .OrderBy(i => i.ApplicationDeadline)
               .ToListAsync(ct);

        public Task<Internship?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Internships
               .AsNoTracking()
               .Include(i => i.Company)
               .Include(i => i.Category)
               .FirstOrDefaultAsync(i => i.Id == id, ct);

        public async Task AddAsync(Internship entity, CancellationToken ct = default)
        {
            await _db.Internships.AddAsync(entity, ct);
        }

        public Task UpdateAsync(Internship entity, CancellationToken ct = default)
        {
            _db.Internships.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Internship entity, CancellationToken ct = default)
        {
            _db.Internships.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _db.SaveChangesAsync(ct) > 0;
        }

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
            _db.Internships.AnyAsync(i => i.Id == id, ct);
    }
}
