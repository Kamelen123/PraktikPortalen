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
        private readonly IInternshipRepository _internshipRepo;
        private readonly IMapper _mapper;

        public InternshipApplicationService(IInternshipApplicationRepository repo, IMapper mapper, IInternshipRepository internshipRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _internshipRepo = internshipRepo;
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
            // 1) Internship exists?
            var internship = await _internshipRepo.GetByIdAsync(dto.InternshipId, ct);
            if (internship is null)
                throw new InvalidOperationException("Internship not found.");

            // 2) Internship is open and not past deadline?
            if (!internship.IsOpen || internship.ApplicationDeadline <= DateTime.UtcNow)
                throw new InvalidOperationException("This internship is closed for applications.");

            // 3) Prevent duplicate application (use the existing repo method)
            var alreadyExists = await _repo.ExistsForApplicantAsync(dto.InternshipId, dto.ApplicantId, ct);
            if (alreadyExists)
                throw new InvalidOperationException("You have already applied for this internship.");

            // 4) Map + set state
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
