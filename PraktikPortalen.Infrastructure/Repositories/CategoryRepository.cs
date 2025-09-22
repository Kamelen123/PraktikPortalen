using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PraktikportalenDbContext _db;
        public CategoryRepository(PraktikportalenDbContext db) => _db = db;

        public Task<List<Category>> GetAllAsync(CancellationToken ct = default) =>
            _db.Categories.AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync(ct);

        public Task<Category?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Categories.FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task AddAsync(Category entity, CancellationToken ct = default) =>
            await _db.Categories.AddAsync(entity, ct);

        public Task UpdateAsync(Category entity, CancellationToken ct = default)
        {
            _db.Categories.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Category entity, CancellationToken ct = default)
        {
            _db.Categories.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _db.SaveChangesAsync(ct) > 0;
    }
}
