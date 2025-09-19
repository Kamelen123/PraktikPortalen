using AutoMapper;
using PraktikPortalen.Application.DTOs.Applications;
using PraktikPortalen.Application.Services.Interfaces;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Enums;
using PraktikPortalen.Domain.Interfaces.Repositories;

namespace PraktikPortalen.Application.Services
{
    public class InternshipApplicationService : IInternshipApplicationService
    {
        private readonly IInternshipApplicationRepository _repo;
        private readonly IMapper _mapper;

        public InternshipApplicationService(IInternshipApplicationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<InternshipApplicationListDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllAsync(ct);
            return _mapper.Map<List<InternshipApplicationListDto>>(list);
        }

        public async Task<List<InternshipApplicationListDto>> GetByApplicantAsync(int applicantId, CancellationToken ct = default)
        {
            var list = await _repo.GetByApplicantAsync(applicantId, ct);
            return _mapper.Map<List<InternshipApplicationListDto>>(list);
        }

        public async Task<InternshipApplicationDetailDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity is null ? null : _mapper.Map<InternshipApplicationDetailDto>(entity);
        }

        public async Task<int> CreateAsync(InternshipApplicationCreateDto dto, CancellationToken ct = default)
        {
            if (await _repo.ExistsForApplicantAsync(dto.InternshipId, dto.ApplicantId, ct))
                throw new InvalidOperationException("Application already exists for this internship and applicant.");

            var entity = _mapper.Map<InternshipApplication>(dto);
            entity.Status = ApplicationStatus.Submitted;
            entity.SubmittedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, InternshipApplicationUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return false;

            _mapper.Map(dto, entity); // maps Status, CoverLetter, CvUrl
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
