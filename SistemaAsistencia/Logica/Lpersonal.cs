﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencia.Logica
{
    public class Lpersonal
    {
        public int Id_personal { get; set; }
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Departamento { get; set; }
        public int Id_cargo { get; set; }
        public double SueldoPorHora { get; set; }
        public string Estado { get; set; }
        public string codigo { get; set; }
    }
}
