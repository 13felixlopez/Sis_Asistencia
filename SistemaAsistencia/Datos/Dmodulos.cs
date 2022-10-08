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
                SqlDataAdapter da = new SqlDataAdapter("Select * from Modulos", Conexion.conectar);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                Conexion.cerrar();
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
                SqlCommand cmd = new SqlCommand("Insertar_Modulos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Modulo", parametros.Modulo);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
    }
}
