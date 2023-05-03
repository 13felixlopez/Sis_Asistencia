using SistemaAsistencia.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SistemaAsistencia
{
    public partial class ConexionRemota : Form
    {
        public ConexionRemota()
        {
            InitializeComponent();
        }
        string cadena_de_conexion;
        string indicador_de_conexion;
        private AES aes = new AES();
        int id;
        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIP.Text))
            {
                conectar_manualmente();
            }
            else
            {
                MessageBox.Show("Ingrese la IP", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void conectar_manualmente()
        {
            string IP = txtIP.Text;
            cadena_de_conexion = "Data Source=" + IP + ";Initial Catalog=Asistencia;Integrated Security=False;User Id=Fermin;Password=13Fermin";
            comprobar_conexion();
            if (indicador_de_conexion=="HAY CONEXION")
            {
                SaveXML(aes.Encrypt(cadena_de_conexion, Desencryptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Conexion Correcta. Vuelve a Abrir el Sistema", "Conxion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
        }
        public void SaveXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter write = new XmlTextWriter("ConnectionString.xml", null);
            write.Formatting = Formatting.Indented;
            doc.Save(write);
            write.Close();
        }
        private void comprobar_conexion()
        {
            try
            {
                SqlConnection conexionManual = new SqlConnection(cadena_de_conexion);
                conexionManual.Open();
                SqlCommand da = new SqlCommand("Select * from Usuarios", conexionManual);
                id = Convert.ToInt32(da.ExecuteScalar());
                indicador_de_conexion = "HAY CONEXION";
            }
            catch (Exception)
            {
                indicador_de_conexion = "NO HAY CONEXION";
            }
        }
    }
}
