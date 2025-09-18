using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PraktikportalenDbContext _db;
        public UserRepository(PraktikportalenDbContext db) => _db = db;

        public Task<List<User>> GetAllAsync(CancellationToken ct = default) =>
            _db.Users.AsNoTracking()
                .OrderBy(u => u.FullName)
                .ToListAsync(ct);

        public Task<User?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, ct);

        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);

        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _db.Users.AddAsync(user, ct);
        }

        public Task UpdateAsync(User entity, CancellationToken ct = default)
        {
            _db.Users.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User entity, CancellationToken ct = default)
        {
            _db.Users.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _db.SaveChangesAsync(ct) > 0;
    }
}
