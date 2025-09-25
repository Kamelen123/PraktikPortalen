using AutoMapper;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Enums;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly IInternshipRepository _repo;
        private readonly IMapper _mapper;

        public InternshipService(IInternshipRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<InternshipListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<InternshipListDto>>(entities);
        }

        public async Task<InternshipDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity is null ? null : _mapper.Map<InternshipDetailDto>(entity);
        }

        public async Task<int> CreateAsync(InternshipCreateDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Internship>(dto);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, InternshipUpdateDto dto, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing is null) return false;

            // map onto the tracked entity
            existing.Title = dto.Title;
            existing.CompanyId = dto.CompanyId;
            existing.CategoryId = dto.CategoryId;
            existing.LocationType = dto.LocationType;
            existing.City = dto.City;
            existing.ApplicationDeadline = dto.ApplicationDeadline;
            existing.IsOpen = dto.IsOpen;
            existing.Description = dto.Description;

            await _repo.UpdateAsync(existing, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _repo.GetByIdAsync(id, ct);
            if (existing is null) return false;

            await _repo.DeleteAsync(existing, ct);
            return await _repo.SaveChangesAsync(ct);
        }

        public async Task<List<InternshipListDto>> GetFilteredAsync(int? categoryId,
        LocationType? locationType, bool? isOpen, CancellationToken ct = default)
        {
            var list = await _repo.GetFilteredAsync(categoryId, locationType, isOpen, ct);
            return _mapper.Map<List<InternshipListDto>>(list);
        }
    }
}
