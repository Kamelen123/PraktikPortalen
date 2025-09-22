using AutoMapper;
using PraktikPortalen.Application.DTOs.Categories;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var categories = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<CategoryListDto>>(categories);
        }

        public async Task<CategoryDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _repo.GetByIdAsync(id, ct);
            return category is null ? null : _mapper.Map<CategoryDetailDto>(category);
        }

        public async Task<int> CreateAsync(CategoryCreateDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Category>(dto);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            await _repo.DeleteAsync(entity, ct);
            return await _repo.SaveChangesAsync(ct);
        }
    }
}
