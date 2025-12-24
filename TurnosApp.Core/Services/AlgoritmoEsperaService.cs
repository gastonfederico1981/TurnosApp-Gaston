using System;
using System.Collections.Generic;
using System.Linq;
using TurnosApp.Core.Entities; // <--- ESTA LÍNEA ES LA QUE FALTA Y ARREGLA EL ERROR

namespace TurnosApp.Core.Services
{
    public class AlgoritmoEsperaService
    {
        // Tu método que calcula el tiempo de espera
        public int CalcularTiempoEstimado(IEnumerable<Turno> turnosRestantes)
        {
            // Lógica del algoritmo (ejemplo: 15 mins por turno)
            return turnosRestantes.Count() * 15;
        }
    }
}