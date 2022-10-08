using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaAsistencia.Logica;
using System.Windows.Forms;

namespace SistemaAsistencia.Datos
{
    /// <summary>
    /// Ya teniendo guardado los diferentes modulos podremos administrar los accesos a los distintos usurios
    /// </summary>
    public class Dpermisos
    {
        /// <summary>
        /// Se seleccionan los modulos a los que el usuario podra acceder y se guarda el id de usuario y el id del modulo que se le otorgo el permiso
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool Insertar_Permisos(Lpermisos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Permisos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdModulo", parametros.IdModulo);
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        /// <summary>
        /// Se manda el id de usuario y se consulta los id de modulos que tiene asignado
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="parametros"></param>
        public void mostrar_Permisos(ref DataTable dt,Lpermisos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Permisos", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
                da.Fill(dt);

                Conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        /// <summary>
        /// Se muetran los modulos que tiene el usuario y al deseleccionar se eliminara el acceso
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool Eliminar_Permisos(Lpermisos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("Eliminar_Permisos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
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
