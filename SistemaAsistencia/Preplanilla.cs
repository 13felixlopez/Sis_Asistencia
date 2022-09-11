using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencia.Datos;

namespace SistemaAsistencia
{
    public partial class Preplanilla : UserControl
    {
        public Preplanilla()
        {
            InitializeComponent();
        }

        private void ReporteAsistencias()
        {
            ReporteAsistencia rpt = new ReporteAsistencia();
            DataTable dt = new DataTable();
            Dasistencias funcion = new Dasistencias();
            funcion.mostrar_asistencias_diarias(ref dt,txtdesde.Value,txthasta.Value,Convert.ToInt32(lblnumerosemana.Text));
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.Report = rpt;
            reportViewer1.RefreshReport();
        }

        private void calcular_numero_de_semana()
        {
            DateTime v2 = txthasta.Value;
            lblnumerosemana.Text = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek).ToString();

        }

        private void Preplanilla_Load(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
        }

        private void txtdesde_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
            txthasta.Text = txtdesde.Text;
        }

        private void txthasta_ValueChanged(object sender, EventArgs e)
        {
            calcular_numero_de_semana();
            ReporteAsistencias();
        }
    }
}
