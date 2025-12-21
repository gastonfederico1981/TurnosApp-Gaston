using System.Collections.Generic;

namespace TurnosApp.Core
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        
        // Relación inversa: Un doctor tiene muchos turnos
        public List<Turno> Turnos { get; set; } = new List<Turno>();
    }
}