using System;

namespace TurnosApp.Core.Entities
{
    public class Turno 
    {
        public int Id { get; set; }
        public string PacienteNombre { get; set; } = string.Empty;
        public string Estado { get; set; } = "En Espera";
        public DateTime FechaHora { get; set; } = DateTime.Now;
        
        // Campo clave para tu idea de la Historia Clínica
        public string? HistoriaClinicaUrl { get; set; }
    }
}