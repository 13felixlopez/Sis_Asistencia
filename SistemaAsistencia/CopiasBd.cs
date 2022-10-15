using SistemaAsistencia.Datos;
using SistemaAsistencia.Logica;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SistemaAsistencia
{
    public partial class CopiasBd : UserControl
    {
        public CopiasBd()
        {
            InitializeComponent();
        }
        string ruta;
        string txtsoftware = "Asistencia";
        string Base_De_datos = "Asistencia";
        private Thread Hilo;
        private bool acaba = false;
        /// <summary>
        /// muestra la ruta donde se guardara la copia de seguridad
        /// </summary>
        public void mostrarRuta()
        {
            DcopiasBd funcion = new DcopiasBd();
            funcion.MostrarRuta(ref ruta);
            txtRuta.Text = ruta;
        }

        private void CopiasBd_Load(object sender, EventArgs e)
        {
            mostrarRuta();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarCopia();
        }

        public void GenerarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                Hilo = new Thread(new ThreadStart(ejecucion));
                Hilo.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Selecciona una Ruta donde Guardar las Copias de Seguridad", "Seleccione Ruta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuta.Focus();
            }
        }

        private void ejecucion()
        {
            string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
            if (System.IO.Directory.Exists(txtRuta.Text + miCarpeta))
            {

            }
            else
            {
                Directory.CreateDirectory(txtRuta.Text + miCarpeta);
            }
            string ruta_completa = txtRuta.Text + miCarpeta;
            
            try
            {
                string v_nombre_respaldo = Base_De_datos + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".bak";
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + ruta_completa + @"\" + v_nombre_respaldo + "'", Conexion.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;

            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);

            }
        }
        private void Label2_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void ObtenerRuta()
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        /// <summary>
        /// timer1_S the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (acaba==true)
            {
                timer1.Stop();
                editarRespaldos();
            }
        }

        /// <summary>
        /// Editar los respaldos.
        /// </summary>
        private void editarRespaldos()
        {
            LCopiasBd parametros = new LCopiasBd();
            DcopiasBd funcion = new DcopiasBd();
            parametros.Ruta = txtRuta.Text;
            if (funcion.EditarCopiasBd(parametros)==true)
            {
                MessageBox.Show("Copia de Base de datos Generada", "Creacion de Copia de Bd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ejecucion2 EL proceso de backup de manera automatica ya que tiene una ruta predeterminada para guardar los respaldos.
        /// </summary>
        public void ejecucion2()
        {
            string Ruta = "C:/";
            string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
            if (Directory.Exists(Ruta + miCarpeta))
            {

            }
            else
            {
                Directory.CreateDirectory(Ruta + miCarpeta);
            }
            string ruta_completa = Ruta + miCarpeta;
            //string SubCarpeta = ruta_completa + @"\Respaldo_al_" + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            //try
            //{
            //    Directory.CreateDirectory(Path.Combine(ruta_completa, SubCarpeta));

            //}
            //catch (Exception)
            //{

            //}
            try
            {
                string v_nombre_respaldo = Base_De_datos + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + ".bak";
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + ruta_completa + @"\" + v_nombre_respaldo + "'", Conexion.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;
            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);
            }
        }
        public void purga()
        {
            string txtsoftware = "Asistencia";
            string Ruta = "C:/";
            string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
            string ruta_completa = Ruta + miCarpeta;
            DirectoryInfo dir = new DirectoryInfo(ruta_completa);
            var files = dir.GetFiles();
            files.AsParallel().Reverse().Skip(3).ForAll((f) => f.Delete());
        }
    }
}
