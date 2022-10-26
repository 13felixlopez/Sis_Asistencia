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
using System.Data.SqlClient;
using System.IO;
using MaterialSkin;

namespace SistemaAsistencia
{
    public partial class MenuPrincipal : MaterialSkin.Controls.MaterialForm
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            Reloj.Start();
            timer1.Start();
            timer1.Interval = 1000000;
        }
        public int Idusuario;
        public string LoginV;
        string Base_De_datos = "Asistencia";
        string Servidor = @".\";
        string ruta;

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            SkinManager.Theme = MaterialSkinManager.Themes.DARK;
            panelBienvenida.Dock = DockStyle.Fill;
            validarPermisos();
        }

        private void validarPermisos()
        {
            DataTable dt = new DataTable();
            Dpermisos funcion = new Dpermisos();
            Lpermisos parametros = new Lpermisos();
            parametros.IdUsuario = Idusuario;
            funcion.mostrar_Permisos(ref dt, parametros);
            btnConsultas.Enabled = false;
            btnPersonal.Enabled = false;
            btnRegistro.Enabled = false;
            btnUsuarios.Enabled = false;
            btnRestaurar.Enabled = false;
            foreach (DataRow rowPermisos in dt.Rows)
            {
                string Modulo = Convert.ToString(rowPermisos["Modulo"]);
                if (Modulo == "PrePlanillas")
                {
                    btnConsultas.Enabled = true;
                }
                if (Modulo=="Usuarios")
                {
                    btnUsuarios.Enabled = true;
                    btnRegistro.Enabled = true;
                }
                if (Modulo=="Personal")
                {
                    btnPersonal.Enabled = true;
                }
                if (Modulo=="Respaldos")
                {
                    btnRestaurar.Enabled = true;
                }
                if (Modulo == "Registro")
                {
                    btnRegistro.Enabled = true;
                }
            }
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            Personal control = new Personal();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            CtlUsuarios control = new CtlUsuarios();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RestaurarBd()
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Backup " + Base_De_datos + "|*.bak";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Backup";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ruta = Path.GetFullPath(dlg.FileName);
                DialogResult pregunta = MessageBox.Show("Usted está a punto de restaurar la base de datos," + "asegurese de que el archivo .bak sea reciente, de" + "lo contrario podría perder información y no podrá" + "recuperarla, ¿desea continuar?", "Restauración de base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (pregunta == DialogResult.Yes)
                {
                    Conexion.abrir();
                    try
                    {
                        string useMaster = "USE master";
                        SqlCommand UseMasterCommand = new SqlCommand(useMaster, Conexion.conectar);
                        UseMasterCommand.ExecuteNonQuery();

                        string Alter1 = string.Format("ALTER DATABASE [{0}] SET Single_User WITH Rollback Immediate", Base_De_datos);
                        SqlCommand AlterCmd = new SqlCommand(Alter1, Conexion.conectar);
                        AlterCmd.ExecuteNonQuery();

                        string Restore = string.Format("RESTORE DATABASE {0} FROM DISK='{1}'", Base_De_datos, ruta);
                        SqlCommand RestoreCmd = new SqlCommand(Restore, Conexion.conectar);
                        RestoreCmd.ExecuteNonQuery();

                        string Alter2 = string.Format("ALTER DATABASE [{0}] SET Multi_User", Base_De_datos);
                        SqlCommand Alter2Cmd = new SqlCommand(Alter2, Conexion.conectar);
                        Alter2Cmd.ExecuteNonQuery();
                        MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Conexion.cerrar();
                    }
                    



                    //SqlConnection cnn = new SqlConnection("Server=" + Servidor + ";database=master; integrated security=yes");
                    //try
                    //{
                    //    Backup rest = new Backup();
                    //    rest.Restore();
                    //    cnn.Open();
                    //    string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + Base_De_datos + "' USE [master] ALTER DATABASE [" + Base_De_datos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + Base_De_datos + "] RESTORE DATABASE " + Base_De_datos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                    //    SqlCommand BorraRestaura = new SqlCommand(Proceso, cnn);
                    //    BorraRestaura.ExecuteNonQuery();
                    //    MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    Dispose();
                    //}
                    //catch (Exception)
                    //{

                    //    RestaurarNoExpress();
                    //}
                    //finally
                    //{
                    //    if (cnn.State == ConnectionState.Open)
                    //    {
                    //        cnn.Close();
                    //    }

                    //}

                }
            }

        }
        private void RestaurarNoExpress()
        {
            Servidor = ".";
            SqlConnection cnn = new SqlConnection("Server=" + Servidor + ";database=master; integrated security=yes");
            try
            {
                Backup rest = new Backup();
                rest.Restore();
                //cnn.Open();
                //string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + Base_De_datos + "' USE [master] ALTER DATABASE [" + Base_De_datos + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + Base_De_datos + "] RESTORE DATABASE " + Base_De_datos + " FROM DISK = N'" + ruta + "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                //SqlCommand BorraRestaura = new SqlCommand(Proceso, cnn);
                //BorraRestaura.ExecuteNonQuery();
                MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();


            }
            catch (Exception)
            {

            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            RestaurarBd();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            Preplanilla control = new Preplanilla();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Dispose();
            TomarAsistencia frm = new TomarAsistencia();
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Backup cb = new Backup();
            cb.ejecucion2();
            cb.purga();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Dispose();
            Login lg = new Login();
            lg.ShowDialog();
        }

        private void Reloj_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
