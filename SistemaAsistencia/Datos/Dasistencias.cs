using SistemaAsistencia.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencia.Datos
{
    public class Dasistencias
    {
        /// <summary>
        /// Metodo que se utiliza para buscar al personal por medio del Id
        /// </summary>
        /// <param name="dt">Es la referencia al datatable que se genera con los datos que se piden</param>
        /// <param name="Idpersonal">Es el dato que se esta consultando</param>
        public void buscarAsistenciasId(ref DataTable dt, int Idpersonal)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarAsistenciasId", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idpersonal", Idpersonal);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Log.Writeerror("Se produjo un error en buscarAsistenciasId ❌❌");
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        /// <summary>
        /// Se valida el estado del personal si esta entrando o saliendo para asi registrarlo en la base de datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool InsertarAsistencias(Lasistencias parametros)
        {
            try
            {
                Conexion.abrir();
                Log.WriteCon("Se abrio la conexion para insertar asistencia");
                SqlCommand cmd = new SqlCommand("Insertar_ASISTENCIAS", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_entrada", parametros.Fecha_entrada);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.Parameters.AddWithValue("@Observacion", parametros.Observacion);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Log.Writeerror("Se produjo un error en la insersion de asistencia ❌❌");
                return false;
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerró la conexion en insertar asistencia 🔐🔐");
            }
        }

        /// <summary>
        /// Se guarda la fecha y hora de su salida y cambia el estado de entrada a salida
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool ConfirmarSalida(Lasistencias parametros)
        {
            try
            {
                Conexion.abrir();
                Log.WriteCon("Se abrio la conexion para confirmar salida");
                SqlCommand cmd = new SqlCommand("ConfirmarSalida", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace); 
                Log.Writeerror("Se produjo un error al confirmar salida ❌❌");
                return false;
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerro la conexion en ConfirmarSalida 🔐🔐");
            }
        }
        /// <summary>
        /// Se consulta el proecedimiento almacenado el cual contiene los calculos de las entradas y salidas por personal
        /// Esto lo muestra a traves de una tabla con los datos requeridos
        /// </summary>
        /// <param name="dt">Instancia a la tabla qeu se genera con el procedimiento almacenado</param>
        /// <param name="desde">Variable que guarda la hora de entrada del personal</param>
        /// <param name="hasta">Variable que guarda la salida del personal</param>
        /// <param name="semana">Calculo de la semana qn la cual se esta haciendo la consulta</param>
        public void mostrar_asistencias_diarias(ref DataTable dt, DateTime desde, DateTime hasta,int semana)
        {
            try
            {
                Conexion.abrir();
                Log.WriteCon("Se abrio la conexion para mostrar Asistencias");
                SqlDataAdapter da = new SqlDataAdapter("mostrar_asistencias_diarias", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@semana", semana);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Writeerror("Se produjo un error en mostrar asistencia ❌❌");
            }
            finally
            {
                Conexion.cerrar();
                Log.WriteCon("Se cerró la conexion en mostrar asistencia 🔐🔐");
            }
        }
    }
}
