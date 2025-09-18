using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PraktikPortalen.Application.DTOs.Companies;
using PraktikPortalen.Application.Services.Interfaces;

namespace PraktikPortalen.Controllers
{
    [ApiController]
    [Route("api/v1/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;
        public CompaniesController(ICompanyService service) => _service = service;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken ct) =>
            Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetDetails(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create/(Admin)")]
        public async Task<IActionResult> Create([FromBody] CompanyCreateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var newId = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetDetails), new { id = newId }, null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/update/(Admin)")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyUpdateDto dto, CancellationToken ct)
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
    }
}
