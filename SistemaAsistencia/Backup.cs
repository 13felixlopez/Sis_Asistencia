using SistemaAsistencia.Datos;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SistemaAsistencia
{
    public class Backup
    {
        string txtsoftware = "Asistencia";
        string Base_De_datos = "Asistencia";
        private bool acaba = false;
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
            if (dir.GetFiles().Length>=3)
            {
                var files = dir.GetFiles();
                files.AsParallel().Reverse().Skip(3).ForAll((f) => f.Delete());
            }
            
        }

        public void Restore()
        {
            Conexion.abrir();
            string useMaster = "USE master";
            SqlCommand UseMasterCommand = new SqlCommand(useMaster, Conexion.conectar);
            UseMasterCommand.ExecuteNonQuery();

            string Alter1 = string.Format("ALTER DATABASE [{0}] SET Single_User WITH Rollback Unmediate", Conexion.conectar);
        }
    }
}
