﻿using PraktikPortalen.Domain.Entities;

namespace PraktikPortalen.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken ct = default);
        Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Category entity, CancellationToken ct = default);
        Task UpdateAsync(Category entity, CancellationToken ct = default);
        Task DeleteAsync(Category entity, CancellationToken ct = default);
        Task<bool> SaveChangesAsync(CancellationToken ct = default);
    }
}
