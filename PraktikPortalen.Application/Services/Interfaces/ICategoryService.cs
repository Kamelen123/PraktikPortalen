using PraktikPortalen.Application.DTOs.Categories;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryListDto>> GetAllAsync(CancellationToken ct = default);
        Task<CategoryDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(CategoryCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, CategoryUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
