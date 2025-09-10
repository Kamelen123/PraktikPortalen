using Microsoft.AspNetCore.Mvc;
using PraktikPortalen.Application.DTOs.Internships;
using PraktikPortalen.Application.Services.Interfaces;

namespace PraktikPortalen.Api.Controllers
{
    [ApiController]
    [Route("api/v1/internships")]
    public class InternshipsController : ControllerBase
    {
        private readonly IInternshipService _service;

        public InternshipsController(IInternshipService service) => _service = service;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken ct) =>
            Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:int}/details")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] InternshipCreateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var newId = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:int}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] InternshipUpdateDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await _service.UpdateAsync(id, dto, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}/delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
