using Microsoft.EntityFrameworkCore;
using PraktikPortalen.Domain.Entities;
using PraktikPortalen.Domain.Interfaces.Repositories;
using PraktikPortalen.Infrastructure.Data;

namespace PraktikPortalen.Infrastructure.Repositories
{
    public class InternshipApplicationRepository : IInternshipApplicationRepository
    {
        private readonly PraktikportalenDbContext _db;
        public InternshipApplicationRepository(PraktikportalenDbContext db) => _db = db;

        public Task<List<InternshipApplication>> GetAllAsync(CancellationToken ct = default) =>
            _db.InternshipApplications.AsNoTracking()
               .Include(a => a.Internship).ThenInclude(i => i.Company)
               .Include(a => a.Applicant)
               .OrderByDescending(a => a.SubmittedAt)
               .ToListAsync(ct);

        public Task<List<InternshipApplication>> GetByApplicantAsync(int applicantId, CancellationToken ct = default) =>
            _db.InternshipApplications.AsNoTracking()
               .Where(a => a.ApplicantId == applicantId)
               .Include(a => a.Internship).ThenInclude(i => i.Company)
               .OrderByDescending(a => a.SubmittedAt)
               .ToListAsync(ct);

        public Task<InternshipApplication?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _db.InternshipApplications.AsNoTracking()
               .Include(a => a.Internship).ThenInclude(i => i.Company)
               .Include(a => a.Applicant)
               .FirstOrDefaultAsync(a => a.Id == id, ct);

        public async Task AddAsync(InternshipApplication entity, CancellationToken ct = default) =>
            await _db.InternshipApplications.AddAsync(entity, ct);

        public Task UpdateAsync(InternshipApplication entity, CancellationToken ct = default)
        {
            _db.InternshipApplications.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(InternshipApplication entity, CancellationToken ct = default)
        {
            _db.InternshipApplications.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken ct = default) =>
            await _db.SaveChangesAsync(ct) > 0;

        public Task<bool> ExistsForApplicantAsync(int internshipId, int applicantId, CancellationToken ct = default) =>
            _db.InternshipApplications.AnyAsync(a => a.InternshipId == internshipId && a.ApplicantId == applicantId, ct);
    }
}