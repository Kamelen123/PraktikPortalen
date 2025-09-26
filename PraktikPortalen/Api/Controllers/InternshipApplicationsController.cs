using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikPortalen.Application.DTOs.Applications;
using PraktikPortalen.Application.Services.Interfaces;

namespace PraktikPortalen.Controllers
{
    [ApiController]
    [Route("api/v1/applications")]
    public class InternshipApplicationsController : ControllerBase
    {
        private readonly IInternshipApplicationService _service;
        public InternshipApplicationsController(IInternshipApplicationService service) => _service = service;

        [Authorize(Roles = "Admin")]
        [HttpGet("all/(Admin)")]
        public async Task<IActionResult> GetAll(CancellationToken ct) =>
            Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetDetails(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpPost("create/(Member, Admin)")]
        public async Task<IActionResult> Create([FromBody] InternshipApplicationCreateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);


            var id = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetDetails), new { id }, null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/update/(Admin)")]
        public async Task<IActionResult> Update(int id, [FromBody] InternshipApplicationUpdateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await _service.UpdateAsync(id, dto, ct);
            return ok ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/delete/(Admin)")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpGet("by-applicant/{applicantId:int}/(Member, Admin)")]
        public async Task<IActionResult> GetByApplicant(int applicantId, CancellationToken ct) =>
            Ok(await _service.GetByApplicantAsync(applicantId, ct));
    }
}
