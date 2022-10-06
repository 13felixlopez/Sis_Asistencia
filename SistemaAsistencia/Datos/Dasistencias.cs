﻿using SistemaAsistencia.Logica;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaAsistencia.Datos
{
    public class Dasistencias
    {
        /// <summary>
        /// Metodo que se utiliza para buscar al personal por medio del Id
        /// </summary>
        /// <param name="dt">Es la referencia al datatable que se genera con los datos que se piden</param>
        /// <param name="Idpersonal">Es el dato que se esta consultando</param>
        public void buscarAsistenciasId(ref DataTable dt, int Idpersonal)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarAsistenciasId", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idpersonal", Idpersonal);
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
        /// Se valida el estado del personal si esta entrando o saliendo para asi registrarlo en la base de datos
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool InsertarAsistencias(Lasistencias parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_ASISTENCIAS", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_entrada", parametros.Fecha_entrada);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.Parameters.AddWithValue("@Observacion", parametros.Observacion);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                Conexion.abrir();
            }
        }

        /// <summary>
        /// Se guarda la fecha y hora de su salida y cambia el estado de entrada a salida
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public bool ConfirmarSalida(Lasistencias parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("ConfirmarSalida", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void mostrar_asistencias_diarias(ref DataTable dt, DateTime desde, DateTime hasta,int semana)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_asistencias_diarias", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@semana", semana);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
    }
}
