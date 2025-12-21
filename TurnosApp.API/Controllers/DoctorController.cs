using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;
using TurnosApp.Core;
using TurnosApp.Core.Interfaces;

namespace TurnosApp.API.Controllers
{
    [Route("api/turnos")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly TurnosDbContext _context;
        private readonly ITurnoService _turnoService;

        public TurnosController(TurnosDbContext context, ITurnoService turnoService)
        {
            _context = context;
            _turnoService = turnoService;
        }

        // 1. Obtener lista para el Doctor
        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnosByDoctor(int doctorId)
        {
            return await _context.Turnos
                .Where(t => t.DoctorId == doctorId && t.Estado == "PENDIENTE")
                .ToListAsync();
        }

        // 2. Acción de Llamar/Atender (El que usa tu botón)
        [HttpPut("{id}/atender")]
        public async Task<IActionResult> Atender(int id)
        {
            var actualizado = await _turnoService.AtenderTurnoAsync(id);
            if (!actualizado) return NotFound();
            return Ok();
        }

        // 3. Crear turno (El que usas en Swagger)
        [HttpPost]
        public async Task<ActionResult<Turno>> Create([FromBody] Turno turno)
        {
            var nuevoTurno = await _turnoService.RegisterTurnoAsync(turno);
            return CreatedAtAction(nameof(GetTurnosByDoctor), new { doctorId = turno.DoctorId }, nuevoTurno);
        }
    }

    public class StatusUpdateDto
    {
        public string Estado { get; set; } = string.Empty;
    }
}