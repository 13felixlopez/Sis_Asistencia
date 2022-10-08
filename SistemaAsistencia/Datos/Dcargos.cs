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
    /// Esta clase es la cual tiene el CRUD de los cargos del personal
    /// </summary>
    public class Dcargos
    {
        /// <summary>
        /// Metodo que inserta un nuevo cargo, teniendo como requisitos el nombre del cargo y el sueldo por hora
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool insertar_Cargo(Lcargos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Cargo", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
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
        /// <summary>
        /// Editar Cargos se hace mediante el id del cargo que se le envie a la base de datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool editar_Cargo(Lcargos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("editar_Cargo", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", parametros.Id_cargo);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Cargo);
                cmd.Parameters.AddWithValue("@Sueldo", parametros.SueldoPorHora);
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
        /// <summary>
        /// EL texto que se escriba en el textbox de busqueda se compara con los cargos almacenados en la BD y se muestra el que coincida con lo escrito,
        /// En caso que no haya coincidencias el resultado sera un tabla vacia
        /// </summary>
        /// <param name="dt">Tabla resultante</param>
        /// <param name="buscador">Variable que contiene el texto a buscar</param>
        public void buscarCargos(ref DataTable dt, string buscador)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarCargos", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
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
    }
}
