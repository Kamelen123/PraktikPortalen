using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PraktikPortalen.Application.DTOs.Users;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher<User> _hasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IPasswordHasher<User> hasher, IMapper mapper)
        {
            _repo = repo;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<List<UserListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var users = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<UserListDto>>(users);
        }

        public async Task<UserDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var user = await _repo.GetByIdAsync(id, ct);
            return user is null ? null : _mapper.Map<UserDetailDto>(user);
        }

        public async Task<int> CreateAsync(UserCreateDto dto, CancellationToken ct = default)
        {
            // Map input → entity (PasswordHash ignored by profile)
            var entity = _mapper.Map<User>(dto);

            // Hash the incoming password
            entity.PasswordHash = _hasher.HashPassword(entity, dto.Password);

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            // Map onto the tracked entity (updates FullName/Role/IsActive)
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
