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
    public class Dpersonal
    {
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
