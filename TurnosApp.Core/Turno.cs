using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurnosApp.Core
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PacienteNombre { get; set; } = string.Empty;

        public string PacienteTelefono { get; set; } = string.Empty;

        // ID del doctor (clave foránea)
        public int DoctorId { get; set; }

        // ESTO ES LO QUE FALTA: La propiedad de navegación
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; } 

        [Required]
        public DateTime FechaProgramada { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        public string Estado { get; set; } = "PENDIENTE";

        public string TokenAcceso { get; set; } = string.Empty;

        public DateTime? FechaFinConsulta { get; set; }

        public int TiempoEsperaEstimado { get; set; } 
    }
}