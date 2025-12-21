using System;
using System.Collections.Generic;
using TurnosApp.Core; // <--- ESTA LÍNEA ES VITAL para que encuentre 'Turno'

namespace TurnosApp.Core.Services
{
    public class AlgoritmoEsperaService
    {
        public void CalcularTiempoEspera(Turno turnoActual, List<Turno> otrosTurnos, string especialidad)
        {
            // 1. Definimos tiempo base por especialidad
            double minutosBase = (especialidad == "Cardiología") ? 20.0 : 15.0;
            
            // 2. Lógica: (cantidad de personas antes + 1) * tiempo base
            double totalMinutos = (otrosTurnos.Count + 1) * minutosBase;
            
            // 3. Guardamos el resultado como entero (int)
            turnoActual.TiempoEsperaEstimado = (int)totalMinutos;
        }
    }
}