using Microsoft.EntityFrameworkCore;
using TurnosApp.Core;
using TurnosApp.Core.Interfaces;
using TurnosApp.Infrastructure.Data;

namespace TurnosApp.Infrastructure.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly TurnosDbContext _context;

        public TurnoService(TurnosDbContext context)
        {
            _context = context;
        }

        // 1. Implementación de la lista para el doctor
        public async Task<IEnumerable<Turno>> GetTurnosByDoctorAsync(int doctorId)
        {
            return await _context.Turnos
                .Where(t => t.DoctorId == doctorId && t.Estado == "PENDIENTE")
                .ToListAsync();
        }

        // 2. Implementación para registrar turnos (Swagger)
        public async Task<Turno> RegisterTurnoAsync(Turno turno)
        {
            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();
            return turno;
        }

        // 3. Implementación para atender (Botón Llamar)
        public async Task<bool> AtenderTurnoAsync(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null) return false;

            turno.Estado = "EN_CONSULTA";
            await _context.SaveChangesAsync();
            return true;
        }
    }
}