using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;

namespace TurnosApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicController : ControllerBase
    {
        private readonly TurnosDbContext _context;

        public PublicController(TurnosDbContext context)
        {
            _context = context;
        }

        [HttpGet("seguimiento/{token}")]
        public async Task<IActionResult> ObtenerSeguimiento(string token)
        {
            var turno = await _context.Turnos
                .Include(t => t.Doctor)
                .FirstOrDefaultAsync(t => t.TokenAcceso == token);

            if (turno == null) return NotFound();

            return Ok(new {
                paciente = turno.PacienteNombre,
                doctor = turno.Doctor?.Nombre,
                tiempo = turno.TiempoEsperaEstimado
            });
        }
    }
}