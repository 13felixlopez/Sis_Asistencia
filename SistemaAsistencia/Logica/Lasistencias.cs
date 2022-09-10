using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencia.Logica
{
    public class Lasistencias
    {
        public int Id_asistencia { get; set; }
        public int Id_personal { get; set; }
        public DateTime Fecha_entrada { get; set; }
        public DateTime Fecha_salida { get; set; }
        public string Estado { get; set; }
        public double Horas { get; set; }
        public string Observaciones { get; set; }
    }
}
