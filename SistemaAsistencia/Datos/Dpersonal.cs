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
	/// CRUD del personal para control de entradas y salidas, y calculo de horas 
	/// </summary>
    public class Dpersonal
    {
		/// <summary>
		/// Se recogen los datos necesarios para agregar un nuevo personal y se le asigna un cargo
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns></returns>
		public bool InsertarPersonal(Lpersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("InsertarPersonal", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
				cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
				cmd.Parameters.AddWithValue("@Departamento", parametros.Departamento);
				cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
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
		/// Se edita el personal por medio del id que se mande a la bd.
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns></returns>
		public bool editarPersonal(Lpersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("editarPersonal", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
				cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
				cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
				cmd.Parameters.AddWithValue("@Departamento", parametros.Departamento);
				cmd.Parameters.AddWithValue("@Id_cargo", parametros.Id_cargo);
				cmd.Parameters.AddWithValue("@Sueldo_por_hora", parametros.SueldoPorHora);
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
		/// Se elimina o se desactiva el personal que sea igual al id que se mande a la bd
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns></returns>
		public bool eliminarPersonal(Lpersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("eliminarPersonal", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal); ;
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
		/// Metodo que se utiliza para mostrar el personal y se realiza un paginado donde se muestra el personal de 10 en 10
		/// Por eso se utilizan las variables "desde" y "hasta", en ellas se guardan las posiciones que se estan mostrando
		/// Tambien se muestra el cargo que tiene cada persona
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="desde"></param>
		/// <param name="hasta"></param>
		public void MostrarPersonal(ref DataTable dt, int desde, int hasta)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("mostrarPersonal", Conexion.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
				da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
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
		/// Se busca a un trabajador en especifico por medio del nombre
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="desde"></param>
		/// <param name="hasta"></param>
		/// <param name="buscador">Variable que contiene el nombre a buscar</param>
		public void BuscarPersonal(ref DataTable dt, int desde, int hasta, string buscador)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", Conexion.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
				da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
				da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
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
		/// Se busca a un trabajador en especifico por medio de la identificacion
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="buscador"></param>
		public void BuscarPersonalIdentidad(ref DataTable dt, string buscador)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("BuscarPersonalIdentidad", Conexion.conectar);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
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
		/// Ya que no se elimina el personal en la base de datos se puede restaurar y se cambia el estado a actico por medio del id
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns>Si se retorna un false es porque el id que se manda a la bd no existe</returns>
		public bool restaurar_personal(Lpersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("restaurar_personal", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal); ;
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
		/// Se hace un conteo del personal ingresado para hacer la division y poderlo paginar
		/// </summary>
		/// <param name="Contador"></param>
		public void ContarPersonal(ref int Contador)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("select Count(Id_personal) from Personal", Conexion.conectar);
				Contador = Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch (Exception)
			{
				Contador = 0;
			}
			finally
			{
				Conexion.cerrar();
			}
		}
	}
}
