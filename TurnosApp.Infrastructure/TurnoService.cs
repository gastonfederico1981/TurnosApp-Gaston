using TurnosApp.Core.Entities;
using TurnosApp.Core.Interfaces;
using TurnosApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TurnosApp.Infrastructure.Services 
{
    public class TurnoService : ITurnoService
    {
        private readonly TurnosDbContext _context;

        public TurnoService(TurnosDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Turno>> GetTurnosAsync() 
            => await _context.Turnos.ToListAsync();

        public async Task<Turno?> GetTurnoByIdAsync(int id) 
            => await _context.Turnos.FindAsync(id);

        // Esta es la pieza clave para tu idea de la Historia Clínica
        public async Task ActualizarHistoriaClinicaAsync(int id, string url)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno != null)
            {
                turno.HistoriaClinicaUrl = url;
                await _context.SaveChangesAsync();
            }
        }
    }
}