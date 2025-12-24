using System.Collections.Generic;

namespace TurnosApp.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        
        // ESTAS DOS PROPIEDADES SON LAS QUE FALTAN:
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        
        public List<Turno> Turnos { get; set; } = new List<Turno>();
    }
}