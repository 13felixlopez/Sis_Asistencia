﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using SistemaAsistencia.Datos;
using SistemaAsistencia.Logica;
using System.Threading;

namespace SistemaAsistencia
{
    public partial class CopiasBd : UserControl
    {
        public CopiasBd()
        {
            InitializeComponent();
        }
        string ruta;
        string txtsoftware = "Asistencia";
        string Base_De_datos = "ASISTENCIA";
        private Thread Hilo;
        private bool acaba = false;

        private void mostrarRuta()
        {
            DcopiasBd funcion = new DcopiasBd();
            funcion.MostrarRuta(ref ruta);
            txtRuta.Text = ruta;
        }

        private void CopiasBd_Load(object sender, EventArgs e)
        {
            mostrarRuta();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarCopia();
        }

        private void GenerarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                Hilo = new Thread(new ThreadStart(ejecucion));
                Hilo.Start();
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Selecciona una Ruta donde Guardar las Copias de Seguridad", "Seleccione Ruta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRuta.Focus();
            }
        }

        private void ejecucion()
        {
            string miCarpeta = "Copias_de_Seguridad_de_" + txtsoftware;
            if (System.IO.Directory.Exists(txtRuta.Text + miCarpeta))
            {

            }
            else
            {
                System.IO.Directory.CreateDirectory(txtRuta.Text + miCarpeta);
            }
            string ruta_completa = txtRuta.Text + miCarpeta;
            string SubCarpeta = ruta_completa + @"\Respaldo_al_" + DateTime.Now.Day + "_" + (DateTime.Now.Month) + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(ruta_completa, SubCarpeta));

            }
            catch (Exception)
            {

            }
            try
            {
                string v_nombre_respaldo = Base_De_datos + ".bak";
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + Base_De_datos + " TO DISK = '" + SubCarpeta + @"\" + v_nombre_respaldo + "'", Conexion.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;

            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);

            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void ObtenerRuta()
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ObtenerRuta();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (acaba==true)
            {
                timer1.Stop();
                editarRespaldos();
            }
        }

        private void editarRespaldos()
        {
            LCopiasBd parametros = new LCopiasBd();
            DcopiasBd funcion = new DcopiasBd();
            parametros.Ruta = txtRuta.Text;
            if (funcion.EditarCopiasBd(parametros)==true)
            {
                MessageBox.Show("Copia de Base de datos Generada", "Creacion de Copia de Bd", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}