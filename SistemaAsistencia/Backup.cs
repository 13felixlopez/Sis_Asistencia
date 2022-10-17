using SistemaAsistencia.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAsistencia
{
    public class Backup
    {
        string ruta;
        string txtsoftware = "Asistencia";
        string Base_De_datos = "Asistencia";
        private Thread Hilo;
        private bool acaba = false;
        public void GenerarCopia()
        {
            Hilo = new Thread(new ThreadStart(ejecucion2));
            Hilo.Start();
        }
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
