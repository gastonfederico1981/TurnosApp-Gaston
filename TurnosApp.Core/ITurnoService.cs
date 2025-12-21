using System.Collections.Generic;
using System.Threading.Tasks;
using TurnosApp.Core;

namespace TurnosApp.Core.Interfaces
{
    public interface ITurnoService
    {
        Task<IEnumerable<Turno>> GetTurnosByDoctorAsync(int doctorId);
        Task<Turno> RegisterTurnoAsync(Turno turno);
        Task<bool> AtenderTurnoAsync(int id);
    }
}