using Microsoft.EntityFrameworkCore;
using TurnosApp.Core.Entities;
using TurnosApp.Core; // Asegúrate de que apunte a donde está la clase Doctor

namespace TurnosApp.Infrastructure.Data
{
    public class TurnosDbContext : DbContext
    {
        public TurnosDbContext(DbContextOptions<TurnosDbContext> options) : base(options) { }

        public DbSet<Turno> Turnos { get; set; }
        
        // AGREGA ESTA LÍNEA PARA SOLUCIONAR EL ERROR:
        public DbSet<Doctor> Doctores { get; set; } 
    }
}