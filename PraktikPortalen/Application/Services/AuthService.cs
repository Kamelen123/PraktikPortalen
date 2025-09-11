using Microsoft.AspNetCore.Identity;
using PraktikPortalen.Application.DTOs.Auth;
using PraktikPortalen.Application.Security;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Enums;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ITokenService _tokens;

        public AuthService(IUserRepository users, IPasswordHasher<User> hasher, ITokenService tokens)
        {
            _users = users;
            _hasher = hasher;
            _tokens = tokens;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            var user = await _users.GetByEmailAsync(dto.Email, ct);
            if (user is null || !user.IsActive) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            var token = _tokens.CreateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(60) // keep in sync with JwtOptions
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {

            var existing = await _users.GetByEmailAsync(dto.Email, ct);
            if (existing is not null) return null;

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                Role = dto.Role == UserRole.Unassigned ? UserRole.Member : dto.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            await _users.AddAsync(user, ct);
            await _users.SaveChangesAsync(ct);

            var token = _tokens.CreateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                ExpiresAtUtc = DateTime.UtcNow.AddMinutes(60)
            };
        }
    }
}
