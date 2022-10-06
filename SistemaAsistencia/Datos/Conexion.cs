using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAsistencia.Datos
{
    /// <summary>
    /// Con esta clase nos conectamos a la base de datos de forma segura
    /// La conexion es cifrada y para poder conectarnos primero se tiene que decencriptar por ello declaramos la variable conexion
    /// La cual consulta la carpeta Logica para utilizar la clase Desencryptacion y validar la conexion
    /// </summary>
    public class Conexion
    {
        public static string conexion = Convert.ToString(Logica.Desencryptacion.checkServer());
        public static SqlConnection conectar = new SqlConnection(conexion);

        /// <summary>
        /// Este metodo valida si la conexion esta decencriptada para poder conectarse a la base de datos y realizar las consultas necesarias
        /// Valida que la conexion este cerrada para abrirla
        /// </summary>
        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }

        /// <summary>
        /// Metodo que cierra la conexion a la base de datos, valida que la conexion este abierta para cerrarla
        /// </summary>
        public static void cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
