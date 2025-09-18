using AutoMapper;
using PraktikPortalen.Application.DTOs.Companies;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repo;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CompanyListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var items = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<CompanyListDto>>(items);
        }

        public async Task<CompanyDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity is null ? null : _mapper.Map<CompanyDetailDto>(entity);
        }

        public async Task<int> CreateAsync(CompanyCreateDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Company>(dto);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, CompanyUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            entity.Name = dto.Name;
            entity.Website = dto.Website;
            entity.City = dto.City;
            entity.IsActive = dto.IsActive;

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
