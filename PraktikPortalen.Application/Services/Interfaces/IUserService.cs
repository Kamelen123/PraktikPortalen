using PraktikPortalen.Application.DTOs.Users;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserListDto>> GetAllAsync(CancellationToken ct = default);
        Task<UserDetailDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<int> CreateAsync(UserCreateDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, UserUpdateDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
