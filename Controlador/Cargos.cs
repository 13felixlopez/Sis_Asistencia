using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    public class Cargos
    {
        int idcargo;
        Boolean tiene_datos;
        DataSet1.CargoRow datos;
        DataSet1TableAdapters.CargoTableAdapter q;

        #region Constructores
        public Cargos()
        {
            this.q = new DataSet1TableAdapters.CargoTableAdapter();
            this.idcargo = -1;
            this.tiene_datos = false;
        }

        public Cargos(int idcargo)
        {
            this.q = new DataSet1TableAdapters.CargoTableAdapter();
            this.idcargo = idcargo;
            this.datos = q.GetDataById_cargo(this.idcargo)[0];
            this.tiene_datos = true;
        }

        #endregion

        #region Datos
        public Int64 Insertar(string Cargo, decimal SueldoPorHora)
        {

            if (q.Connection.State != ConnectionState.Open)
            {
                q.Connection.Open();
            }

            //SqlTransaction trans = q.Connection.BeginTransaction();
            //try
            //{
            q.Insertar(Cargo, SueldoPorHora);
            Int64 last_id = q.GetDataByLastId()[0].Id_cargo;
            // trans.Commit();
            return (last_id);
            //  }
            //catch (Exception ex)
            //{
            //   trans.Rollback();
            //    return -1;
            //}
        }

        public Boolean Actualizar(string Cargo, decimal SueldoPorHora)
        {
            try
            {
                this.q.Actualizar(Cargo, SueldoPorHora, this.idcargo);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public Boolean Eliminar()
        {
            try
            {
                this.q.Eliminar(this.idcargo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean CargoExiste(string nomuser)
        {
            try
            {
                Int64? idverficado = this.q.GetDataByCargo(nomuser)[0].Id_cargo;
                if (idverficado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public Boolean TieneDatos()
        {
            return this.tiene_datos;
        }

        #endregion

        #region Getters
        public Int64 GetidcargoVerificado(string usuario)
        {
            Int64 iduserveri = (Int64)q.GetDataByCargo(usuario)[0].Id_cargo;
            return iduserveri;
        }
        #endregion
    }
}
