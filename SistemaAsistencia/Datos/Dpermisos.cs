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
                Log.WriteCon("Se abrio la conexion en insertarPermisos");
                SqlCommand cmd = new SqlCommand("insertar_Permisos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdModulo", parametros.IdModulo);
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
                cmd.ExecuteNonQuery();
                Log.WritePerso("Se insertaron los permisos ✅✅");
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Writeerror("Se produjo un error en insertarpermisos ❌❌");
                return false;
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerró la conexion en InsertarPermisos 🔐🔐");
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
                Log.WriteCon("Se abrio la conexion en mostrarPermisos");
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Permisos", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
                da.Fill(dt);

                Conexion.cerrar();
                Log.WriteCon("Se cerró la conexion en mostrarPermisos");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Log.Writeerror("Ocurrio un error en mostrarpermisos ❌❌");
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
                Log.WriteCon("Se abrio la conexion en eliminarPermisos");
                SqlCommand cmd = new SqlCommand("Eliminar_Permisos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", parametros.IdUsuario);
                cmd.ExecuteNonQuery();
                Log.WritePerso("Se eliminó permiso ✅✅");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Writeerror("Ocurrio un error al eliminarpermisos ❌❌");
                return false;
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerró la conexion en eliminarPermisos 🔐🔐");
            }
        }
    }
}
