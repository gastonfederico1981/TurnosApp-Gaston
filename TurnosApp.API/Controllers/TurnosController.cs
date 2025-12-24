using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurnosApp.Infrastructure.Data;
using TurnosApp.Core.Entities;

namespace TurnosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly TurnosDbContext _context;

        public TurnosController(TurnosDbContext context)
        {
            _context = context;
        }

        // GET: api/turnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }

        // POST: api/turnos
        [HttpPost]
        public async Task<ActionResult<Turno>> PostTurno(Turno turno)
        {
            try 
            {
                _context.Turnos.Add(turno);
                await _context.SaveChangesAsync();
                return Ok(turno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al guardar en DB", detalle = ex.Message });
            }
        }
    }
}