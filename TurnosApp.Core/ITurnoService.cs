using System.Collections.Generic;
using System.Threading.Tasks;
using TurnosApp.Core.Entities; // <--- ESTA ES LA LÍNEA QUE FALTA

namespace TurnosApp.Core.Interfaces
{
    public interface ITurnoService
    {
        Task<IEnumerable<Turno>> GetTurnosAsync();
        Task<Turno?> GetTurnoByIdAsync(int id);
        // Agregamos el método para la Historia Clínica
        Task ActualizarHistoriaClinicaAsync(int id, string url);
    }
}