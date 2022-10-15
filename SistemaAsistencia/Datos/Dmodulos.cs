using SistemaAsistencia.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencia.Datos
{
    /// <summary>
    /// Otorgar y quitar permisos a los usuarios 
    /// </summary>
    public class Dmodulos
    {
        /// <summary>
        /// Muestra los modulos que contiene el sistema, selecciona los modulos a los que tendra acceso un usuario
        /// </summary>
        /// <param name="dt"></param>
        public void mostrar_Modulos(ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                Log.WriteCon("Se abrio la conexion para mostrar los modulos ✅✅");
                SqlDataAdapter da = new SqlDataAdapter("Select * from Modulos", Conexion.conectar);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Log.Writeerror("Se produjo un error en mostrar_Modulos ❌❌");
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerro la conexion en mostrar los modulos 🔐🔐");
            }
        }
        /// <summary>
        /// Los apartados de nuestro sistema se guardan en una tabla para que puedan ser administrados
        /// Estos modulos se crean directamente en codigo y solo se mandan a la base de datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool Insertar_Modulos(Lmodulos parametros)
        {
            try
            {
                Conexion.abrir();
                Log.WriteCon("Se abrio la conexion en Insertar modulos");
                SqlCommand cmd = new SqlCommand("Insertar_Modulos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Modulo", parametros.Modulo);
                cmd.ExecuteNonQuery();
                Log.WritePerso("Se insertaron los modulos ✅✅");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Writeerror("Se produjo un error en insertar modulos ❌❌");
                return false;
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerro la conexion en insertar modulos 🔐🔐");
            }
        }
    }
}
