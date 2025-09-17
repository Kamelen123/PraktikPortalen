using PraktikPortalen.Application.DTOs.Auth;

namespace PraktikPortalen.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto, CancellationToken ct = default);
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
    }
}
