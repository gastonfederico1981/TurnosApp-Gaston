using Microsoft.EntityFrameworkCore;
using TurnosApp.Core; 

namespace TurnosApp.Infrastructure.Data
{
    public class TurnosDbContext : DbContext
    {
        public TurnosDbContext(DbContextOptions<TurnosDbContext> options)
            : base(options)
        {
        }

        public DbSet<Turno> Turnos { get; set; }
        
        // AGREGAR ESTA LÍNEA: Para que la API reconozca la tabla de Doctores
        public DbSet<Doctor> Doctor { get; set; } 
    }
}