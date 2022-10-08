using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using SistemaAsistencia.Logica;

namespace SistemaAsistencia.Datos
{
	/// <summary>
	/// Clase que se encarga de generar un archivo .bak (una copia de seguridad)
	/// </summary>
	public class DcopiasBd
	{
		/// <summary>
		/// Se consulta el procedimiento almacenado para insertar la copia de seguridad, este se encarga de guardar la ruta donde se almacena el backup
		/// </summary>
		/// <returns></returns>
		public bool InsertarCopiasBd()
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("InsertarCopiasBd", Conexion.conectar);
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
		/// Muestra en un textbox la ruta donde se alamcenara el backup
		/// </summary>
		/// <param name="ruta"></param>
		public void MostrarRuta(ref string ruta)
		{
			try
			{
				Conexion.abrir();
				SqlCommand da = new SqlCommand("select Ruta from CopiasBd", Conexion.conectar);
				ruta = Convert.ToString(da.ExecuteScalar());
				Conexion.cerrar();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
		}
		/// <summary>
		/// Edita la ruta en la que se alamcena el backup ya que cada respaldo lo hace en una carpeta diferente
		/// </summary>
		/// <param name="parametros"></param>
		/// <returns></returns>
		public bool EditarCopiasBd(LCopiasBd parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("EditarCopiasBd", Conexion.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Ruta", parametros.Ruta);
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
