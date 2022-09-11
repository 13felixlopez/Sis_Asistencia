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
	public class DcopiasBd
	{
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
