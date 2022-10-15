using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaAsistencia.Logica;

namespace SistemaAsistencia.Datos
{
    public class Dusuarios
    {
		/// <summary>
		/// Se mandan los datos necesarios para agregar un nuevo usuario y su estado predeterminado es activo para utilizarlo
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns>Si retorna flase es porque faltan datos o hay un error en los datos ingresados</returns>
		public bool InsertarUsuarios(Lusuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("insertar_usuario", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Login", parametros.Login);
				cmd.Parameters.AddWithValue("@Password", parametros.Password);
				cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
				cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
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
		/// Se muestran todos los usuarios ya sea su estado activo o eliminado
		/// </summary>
		/// <param name="dt"></param>
		public void mostrar_Usuarios(ref DataTable dt)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Usuarios", Conexion.conectar);
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
		/// Se muestran unicamente los usuarios que estan activos
		/// </summary>
		/// <param name="dt"></param>
		public void mostrar_Usuarios_Activo(ref DataTable dt)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Usuarios Where Estado='ACTIVO'", Conexion.conectar);
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
		/// Se compeueba el id de usuario para filtrar unicamente al usuario principal
		/// </summary>
		/// <param name="Idusuario"></param>
		/// <param name="Login"></param>
		public void ObtenerIdUsuario(ref int Idusuario, string Login)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Login", Login);
				Idusuario = Convert.ToInt32(cmd.ExecuteScalar());
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
		public bool eliminar_Usuarios(Lusuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("eliminar_usuarios", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@login", parametros.Login);

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
		public bool restaurar_usuario(Lusuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("restaurar_usuario", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
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
		public bool editar_Usuarios(Lusuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("editar_Usuarios", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuario", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Login", parametros.Login);
				cmd.Parameters.AddWithValue("@Password", parametros.Password);
				cmd.Parameters.AddWithValue("@Icono", parametros.Icono);

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
		public void buscar_Usuarios(ref DataTable dt, string buscador)
		{
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscar_usuarios", Conexion.conectar);
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
		public void VerificarUsuarios(ref string Indicador)
		{
			try
			{
				int Iduser;
				Conexion.abrir();
				SqlCommand da = new SqlCommand("Select idUsuario From Usuarios", Conexion.conectar);
				Iduser = Convert.ToInt32(da.ExecuteScalar());
				Conexion.cerrar();
				Indicador = "Correcto";
			}
			catch (Exception)
			{

				Indicador = "Incorrecto";
			}
		}
		/// <summary>
		/// Comprobacion del nombre de usuario y la contraseña para permitir el acceso al sistema
		/// </summary>
		/// <param name="parametros"></param>
		/// <param name="id"></param>
		public void validarUsuario(Lusuarios parametros, ref int id)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("validar_usuario", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@password", parametros.Password);
				cmd.Parameters.AddWithValue("@login", parametros.Login);
				id = Convert.ToInt32(cmd.ExecuteScalar());


			}
			catch (Exception ex)
			{
				id = 0;

			}
			finally
			{
				Conexion.cerrar();
			}
		}
	}
}
