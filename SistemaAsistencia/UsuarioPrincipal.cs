using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencia.Datos;
using SistemaAsistencia.Logica;
using System.IO;
using System.Windows.Forms;

namespace SistemaAsistencia
{
    public partial class UsuarioPrincipal : Form
    {
        public UsuarioPrincipal()
        {
            InitializeComponent();
        }
        int idusuario;

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                if (!string.IsNullOrWhiteSpace(TXTCONTRASEÑA.Text))
                {
                    if (TXTCONTRASEÑA.Text==txtconfirmarcontraseña.Text)
                    {
                        insertarUsuarioDefecto();
                    }
                    else
                    {
                        MessageBox.Show("Las contraseñas no coinciden", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Falta ingresar la contraseña", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TXTCONTRASEÑA.Focus();
                }
            }
            else
            {
                MessageBox.Show("Falta ingresar el nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnombre.Focus();
            }
        }

        private void insertarUsuarioDefecto()
        {
            Lusuarios parametros = new Lusuarios();
            Dusuarios funcion = new Dusuarios();
            parametros.Nombre = txtnombre.Text;
            parametros.Login = TXTUSUARIO.Text;
            parametros.Password = TXTCONTRASEÑA.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if (funcion.InsertarUsuarios(parametros)==true)
            {
                InsertarModulos();
                ObtenerIdUsuario();
                InsertarPermisos();
            }
        }

        private void InsertarModulos()
        {
            Lmodulos parametros = new Lmodulos();
            Dmodulos funcion = new Dmodulos();
            var Modulos = new List<string> { "Usuarios", "Respaldos", "Personal", "PrePlanillas" };
            foreach (var Modulo in Modulos)
            {
                parametros.Modulo = Modulo;
                funcion.Insertar_Modulos(parametros);
            }
        }

        private void InsertarPermisos()
        {
            DataTable dt = new DataTable();
            Dmodulos funcionModulos = new Dmodulos();
            funcionModulos.mostrar_Modulos(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                int idModulo = Convert.ToInt32(row["IdModulo"]);
                Lpermisos parametros = new Lpermisos();
                Dpermisos funcion = new Dpermisos();
                parametros.IdModulo = idModulo;
                parametros.IdUsuario = idusuario;
                funcion.Insertar_Permisos(parametros);
            }
            MessageBox.Show("!LISTO! RECUERDA que para Iniciar Sesión tu Usuario es: " + TXTUSUARIO.Text + " y tu Contraseña es: " + TXTCONTRASEÑA.Text, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Dispose();
            Login frm = new Login();
            frm.ShowDialog();
        }

        private void ObtenerIdUsuario()
        {
            Dusuarios funcion = new Dusuarios();
            funcion.ObtenerIdUsuario(ref idusuario, TXTUSUARIO.Text);
        }
    }
}
